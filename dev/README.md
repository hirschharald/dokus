### Start ts Projekt

```
cd <projekt>
npm i typescript --save-dev
npx tsc --init
```
#### edit tsconfig.json
```
  {
    "compilerOptions": {
        "module": "commonjs",
        "target": "ES6",
        "noImplicitAny": false,
        "sourceMap": true,
        "outDir": "./dist",
        "rootDir": "./src",
        "resolveJsonModule": true,
        "esModuleInterop": true,
        "types": ["node", "css-modules"],
        "jsx": "react"
    },
    "exclude": ["trash/**/*.ts"],
    "include": ["src/**/*.ts" ]
    }
```
#### package.json
```
{
  "name": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "version": "1.0.0",
  "description": "",
  "main": "index.js",
  "scripts": {
    "build": "tsc",
    "watch": "tsc -w",
    "dev": "ts-node-dev --respawn src/server.ts",
    "start": "export DEBUG=wss;node ./dist/mpdclient.js",
    "clean": "rimraf ./dist",
    "rebuild": "npm run clean && npm run build"
  },
  "keywords": [],
  "author": "",
  "license": "ISC",
  "devDependencies": {
    "@types/debug": "^4.1.12",
    
    "@types/node": "^20.12.13",
    "debug": "^4.3.5",
    "nodemon": "^3.1.2",
    "rimraf": "^5.0.7",
    "typescript": "^5.4.5"
  },
  "dependencies": {
    
  }
}

```

