# Express Server

## Projektordner erstellen und initialisieren

```
mkdir mein-express-projekt
cd mein-express-projekt
npm init -y
```
## Express installieren


npm install express

## TypeScript einrichten (optional)

Falls du TypeScript verwenden möchtest:

```
npm install cors dotenv express sqlite3 multer

npm install typescript ts-node @types/node @types/express nodemon @types/cors @types/dotenv @types/multer @types/sqlite3 eslint @typescript-eslint/eslint-plugin @typescript-eslint/parser
--save-dev

npx tsc --init
```
## Grundlegende Projektstruktur erstellen
Copy
```
mein-express-projekt/ 
├── src/
│   ├── app.ts (oder app.js)
├── package.json
└── tsconfig.json (falls TypeScript)

##### app.ts 
import express, { Request, Response } from 'express';
const app = express();
const PORT = 3000;
app.get('/', (req: Request, res: Response) => {
  res.send('Hallo Welt!');
});

app.listen(PORT, () => {
  console.log(`Server läuft auf http://localhost:${PORT}`);
});
##### package.json
"scripts": {
  "start": "node src/app.js",
  "dev": "nodemon src/app.js",
  "ts-start": "ts-node src/app.ts",
  "ts-dev": "nodemon --exec ts-node src/app.ts"
}


```
## .env-Datei erstellen


PORT=3000
DB_CONNECTION_STRING=mongodb://localhost:27017/meine-db