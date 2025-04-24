 ### #  # Kernfunktionen der Signal-API

        #### Nachrichten senden/empfangen

            Per HTTP-POST an eine definierte URL.

        #### Gruppen verwalten

            Gruppen erstellen, Beitritte verwalten.

        #### Anhänge (Bilder, Dateien)

             Base64-kodierte Dateien versenden.

        #### Empfangen von Nachrichten
            Via Webhooks oder Abfragen.



##  Docker
docker run -p 8080:8080 \
  -v ./signal-config:/home/.local/share/signal-cli \
  -e MODE="native" \
  bbernhard/signal-cli-rest-api

## api testen
curl -X POST -H "Content-Type: application/json" \
  -d '{"message": "Hallo!", "number": "+SENDER_NUMMER", "recipients": ["+EMPFAENGER_NUMMER"]}' \
  http://localhost:8080/v2/send

## Endpunkte der API (Auszug)
```
#### Endpoint	        Methode	Beschreibung
/v2/send	            POST	Nachricht an einen Empfänger.
/v2/receive/{number}	GET	    Empfangene Nachrichten abrufen.
/v2/groups/{number}	    GET	    Gruppen auflisten.
```

## Integration in nodered mit Http-request
```
{
  "message": "Nachricht von Node-RED!",
  "number": "+SENDER_NUMMER",
  "recipients": ["+EMPFAENGER_NUMMER"]
}
```
## Trigger z. B. per inject-Node oder MQTT-Input.


## docker-compose
```
version: "3"
services:
  signal-cli-rest-api:
    image: bbernhard/signal-cli-rest-api:latest
    environment:
      - MODE=normal 
      #supported modes: json-rpc, native, normal
      #- AUTO_RECEIVE_SCHEDULE=0 22 * * * 
      #enable this parameter on demand (see description below)
    ports:
      - "8080:8080" #map docker port 8080 to host port 8080.
    volumes:
      - "./volumes/signal:/home/.local/share/signal-cli" 
      
      #map "signal-cli-config" folder on host system into docker container. the folder contains the password and cryptographic keys when a new number is registered
```

## Register client
http://192.168.10.154:8080/v1/qrcodelink?device_name=signal-api


## signal schicken
curl -X POST -H "Content-Type: application/json" 'http://localhost:8080/v2/send' -d '{"message": "Test via Signal API!", "number": "+4915252656045", "recipients": [ "+491722406128" ]}'