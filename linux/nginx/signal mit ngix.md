## Erstelle eine neue Konfigurationsdatei für die Signal-API:
sudo nano /etc/nginx/sites-available/signal-api

## Füge diese Konfiguration ein (angepasst an deine Domain/Ports):
```
server {
    listen 80;
    server_name signal-api.deinedomain.de;  # Ersetze mit deiner Domain oder IP
    return 301 https://$host$request_uri;   # HTTP → HTTPS umleiten
}

server {
    listen 443 ssl;
    server_name signal-api.deinedomain.de;

    # SSL-Zertifikate (Let’s Encrypt oder selbstsigniert)
    ssl_certificate /etc/letsencrypt/live/signal-api.deinedomain.de/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/signal-api.deinedomain.de/privkey.pem;

    # SSL-Einstellungen (starke Verschlüsselung)
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;

    # Basic-Authentifizierung
    auth_basic "Signal-API Zugriff";
    auth_basic_user_file /etc/nginx/.htpasswd;

    # Reverse-Proxy zur Signal-API (läuft auf Port 8080)
    location / {
        proxy_pass http://localhost:8080;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }

    # Zugriff nur aus bestimmten IPs erlauben (optional)
    allow 192.168.10.0/24;  # Nur lokales Netzwerk
    deny all;
}
```

## Aktiviere Konfig
```
sudo ln``` -s /etc/nginx/sites-available/signal-api /etc/nginx/sites-enabled/
sudo nginx -t  # Teste die Konfiguration
sudo systemctl restart nginx
```

##  HTTPS-Zertifikat einrichten (Let’s Encrypt)

    Installiere Certbot:
   
```
sudo apt install certbot python3-certbot-nginx
Hole ein Zertifikat:
sudo certbot --nginx -d signal-api.deinedomain.de

    Certbot fragt nach einer E-Mail-Adresse und bestätigt die Domain.

    Die Zertifikate werden automatisch in die Nginx-Konfiguration eingebunden.

Hinweis: Falls du keine Domain hast, nutze ein selbstsigniertes Zertifikat:

    sudo openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
      -keyout /etc/ssl/private/nginx-selfsigned.key \
      -out /etc/ssl/certs/nginx-selfsigned.crt

        Passe dann die Nginx-Konfiguration an, um auf diese Dateien zu verweisen.

```

##  Node-RED anpassen (HTTPS + Auth)
```
Konfiguriere den http request-Node in Node-RED:

    URL: https://signal-api.deinedomain.de/v2/send

    Method: POST

    Headers:
    json

{
  "Content-Type": "application/json",
  "Authorization": "Basic BASE64_ENCODED_CREDENTIALS"  
  ########## Benutzername:Passwort als Base64
}

    Erzeuge die Base64-Credentials mit:
    bash

echo -n "signal-api-user:deinpasswort" | base64
```