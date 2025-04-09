
# Projektstruktur

```
my-express-app/
├── src/
│   ├── controllers/
│   │   └── upload.controller.ts
│   ├── routes/
│   │   └── upload.routes.ts
│   ├── middlewares/
│   │   ├── error.middleware.ts
│   │   └── upload.middleware.ts
│   ├── services/
│   │   └── database.service.ts
│   ├── app.ts
│   └── server.ts
├── .eslintrc.json
├── .env
├── package.json
├── tsconfig.json
└── uploads/
```
## package.json
```
{
  "name": "express-sqlite-example",
  "version": "1.0.0",
  "scripts": {
    "start": "ts-node src/server.ts",
    "dev": "nodemon src/server.ts",
    "lint": "eslint . --ext .ts",
    "build": "tsc"
  },
  "dependencies": {
    "express": "^4.18.2",
    "sqlite3": "^5.1.6",
    "dotenv": "^16.3.1",
    "cors": "^2.8.5",
    "multer": "^1.4.5-lts.1"
  },
  "devDependencies": {
    "@types/express": "^4.17.17",
    "@types/cors": "^2.8.13",
    "@types/multer": "^1.4.7",
    "@types/node": "^20.5.1",
    "@types/sqlite3": "^3.1.8",
    "typescript": "^5.1.6",
    "nodemon": "^3.0.1",
    "eslint": "^8.47.0",
    "@typescript-eslint/eslint-plugin": "^6.4.1",
    "@typescript-eslint/parser": "^6.4.1",
    "ts-node": "^10.9.1"
  }
}
```
## ESLint Konfiguration (.eslintrc.json)
```

{
  "root": true,
  "parser": "@typescript-eslint/parser",
  "plugins": ["@typescript-eslint"],
  "extends": [
    "eslint:recommended",
    "plugin:@typescript-eslint/recommended"
  ],
  "rules": {
    "@typescript-eslint/no-explicit-any": "off",
    "@typescript-eslint/explicit-function-return-type": "warn"
  },
  "env": {
    "node": true
  }
}

```
## Database Service (src/services/database.service.ts)
```
import { open } from 'sqlite';
import sqlite3 from 'sqlite3';

class DatabaseService {
  private static instance: DatabaseService;
  private db: any;

  private constructor() {}

  public static async getInstance(): Promise<DatabaseService> {
    if (!DatabaseService.instance) {
      DatabaseService.instance = new DatabaseService();
      await DatabaseService.instance.initialize();
    }
    return DatabaseService.instance;
  }

  private async initialize() {
    this.db = await open({
      filename: './database.db',
      driver: sqlite3.Database
    });

    await this.db.exec(`
      CREATE TABLE IF NOT EXISTS uploads (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        user_id TEXT NOT NULL,
        image_path TEXT NOT NULL,
        timestamp TEXT NOT NULL,
        description TEXT
      )
    `);
  }

  public getDB() {
    return this.db;
  }
}

export default DatabaseService;
```
## Upload Middleware (src/middlewares/upload.middleware.ts)
```

import multer from 'multer';
import path from 'path';
import { Request } from 'express';

const storage = multer.diskStorage({
  destination: (req: Request, file, cb) => {
    const uploadPath = path.join(__dirname, '../../uploads');
    cb(null, uploadPath);
  },
  filename: (req: Request, file, cb) => {
    cb(null, `${Date.now()}${path.extname(file.originalname)}`);
  }
});

const fileFilter = (
  req: Request, 
  file: Express.Multer.File, 
  cb: multer.FileFilterCallback
) => {
  const allowedTypes = ['image/jpeg', 'image/png', 'image/gif'];
  if (allowedTypes.includes(file.mimetype)) {
    cb(null, true);
  } else {
    cb(new Error('Invalid file type'));
  }
};

const upload = multer({ 
  storage, 
  fileFilter,
  limits: { fileSize: 5 * 1024 * 1024 } // 5MB
});

export default upload;
```
## Upload Controller (src/controllers/upload.controller.ts)
```

import { Request, Response, NextFunction } from 'express';
import DatabaseService from '../services/database.service';

class UploadController {
  public static async uploadFile(
    req: Request, 
    res: Response, 
    next: NextFunction
  ) {
    try {
      if (!req.file) {
        throw new Error('No file uploaded');
      }

      const { userId, timestamp, description } = req.body;
      const filePath = `/uploads/${req.file.filename}`;

      const dbService = await DatabaseService.getInstance();
      const db = dbService.getDB();

      await db.run(
        'INSERT INTO uploads (user_id, image_path, timestamp, description) VALUES (?, ?, ?, ?)',
        [userId, filePath, timestamp, description]
      );

      res.status(201).json({ 
        success: true,
        message: 'File uploaded successfully',
        data: { filePath }
      });
    } catch (error) {
      next(error);
    }
  }

  public static async getUploads(
    req: Request, 
    res: Response, 
    next: NextFunction
  ) {
    try {
      const dbService = await DatabaseService.getInstance();
      const db = dbService.getDB();

      const uploads = await db.all('SELECT * FROM uploads');
      
      res.status(200).json({
        success: true,
        data: uploads
      });
    } catch (error) {
      next(error);
    }
  }
}

export default UploadController;
```
## Upload Routes (src/routes/upload.routes.ts)

```
import { Router } from 'express';
import UploadController from '../controllers/upload.controller';
import uploadMiddleware from '../middlewares/upload.middleware';

const router = Router();

router.post(
  '/upload', 
  uploadMiddleware.single('image'), 
  UploadController.uploadFile
);

router.get(
  '/uploads',
  UploadController.getUploads
);

export default router;
```
## Error Middleware (src/middlewares/error.middleware.ts)
```

import { Request, Response, NextFunction } from 'express';

interface CustomError extends Error {
  status?: number;
}

function errorMiddleware(
  err: CustomError,
  req: Request,
  res: Response,
  next: NextFunction
) {
  const status = err.status || 500;
  const message = err.message || 'Something went wrong';

  console.error('[ERROR]', status, message);

  res.status(status).json({
    success: false,
    status,
    message
  });
}

export default errorMiddleware;

```
## App Setup (src/app.ts)
```

import express, { Application } from 'express';
import cors from 'cors';
import dotenv from 'dotenv';
import uploadRoutes from './routes/upload.routes';
import errorMiddleware from './middlewares/error.middleware';

dotenv.config();

class App {
  public app: Application;

  constructor() {
    this.app = express();
    this.initializeMiddlewares();
    this.initializeRoutes();
    this.initializeErrorHandling();
  }

  private initializeMiddlewares() {
    this.app.use(cors());
    this.app.use(express.json());
    this.app.use(express.urlencoded({ extended: true }));
    this.app.use('/uploads', express.static('uploads'));
  }

  private initializeRoutes() {
    this.app.use('/api', uploadRoutes);
  }

  private initializeErrorHandling() {
    this.app.use(errorMiddleware);
  }
}

export default new App().app;
```
## Server Entry Point (src/server.ts)

```
import app from './app';
import DatabaseService from './services/database.service';

const PORT = process.env.PORT || 3000;

async function startServer() {
  try {
    await DatabaseService.getInstance();
    app.listen(PORT, () => {
      console.log(`Server running on http://localhost:${PORT}`);
    });
  } catch (error) {
    console.error('Failed to start server:', error);
    process.exit(1);
  }
}

startServer();
```
## .env Datei
```
PORT=3000

```
```
npm install

    Entwicklungsserver starten:

npm run dev

    ESLint ausführen:

npm run lint

Features im Überblick:

    Strukturierte Architektur mit Controllern, Services und Routen
    SQLite Integration mit Singleton-Pattern für DB-Zugriff
    Middleware für File-Uploads mit Multer
    Globales Error Handling mit eigenem Middleware
    TypeScript Support mit Typen für alle Komponenten
    ESLint Konfiguration für Code-Qualität
    Environment Variablen mit dotenv
    CORS Unterstützung


