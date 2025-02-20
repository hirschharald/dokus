## Expo Go Projet

#### on Web

````
npx create-expo-app eas --template blank-typescript
cd eas

npx expo install react-dom react-native-web @expo/metro-runtime


````


## EAS Projekt


````
npx create-expo-app eas --template blank-typescript
cd eas
npm i -g eas-cli@latest

eas -v
eas login
eas whoami

eas init
eas build:configure
edit .gitignode add /ios /android

npm i -g eas-cli@latest


eas build --profile development --platform android


npm install --global @expo/ngrok@^4.1.0
npx expo start --tunnel

````

````
npx expo prebuild --clean
eas build --profile development --platform android
```
