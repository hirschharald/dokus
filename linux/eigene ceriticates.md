### erstellen

openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365 -nodes


### Dieser Befehl erstellt zwei Dateien:

    ### key.pem: Der private Schlüssel für das Zertifikat.
    ### cert.pem: Das selbstsignierte SSL-Zertifikat.

```

    Fügen Sie nun in Ihrer Nginx-Konfigurationsdatei (z.B. ./nginx/conf.d/default.conf) die folgenden Zeilen hinzu, um Nginx das Zertifikat und den Schlüssel bekannt zu machen:

```   

    ssl_certificate /etc/nginx/ssl/cert.pem;
    ssl_certificate_key /etc/nginx/ssl/key.pem;
### Server mit http
```
const https = require('https');
const fs = require('fs');
const express = require('express');

const app = express();

const options = {
  key: fs.readFileSync('key.pem'),
  cert: fs.readFileSync('cert.pem')
};

https.createServer(options, app).listen(443, () => {
  console.log('HTTPS-Server läuft auf https://localhost');
});
```

### ngimx
```
server {
    listen 443 ssl;
    server_name localhost 192.168.10.113;  # oder deine Domain
    
    # Redirect all HTTP traffic to HTTPS
    return 301 https://$host$request_uri;

    # Pfad zu deinem SSL-Zertifikat und privaten Schlüssel
    ssl_certificate /pfad/zu/cert.pem;
    ssl_certificate_key /pfad/zu/key.pem;

    # SSL-Einstellungen (empfohlene Sicherheit)
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;

    # Root-Verzeichnis deiner Webseite
    root /var/www/html;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    # Service Worker muss über HTTPS geladen werden
    location /sw.js {
        add_header 'Service-Worker-Allowed' '/';
        default_type application/javascript;
    }
}
```
## Lets encrypt
```
sudo apt update
sudo apt install certbot python3-certbot-nginx  # Für Nginx
# Oder für Apache:
# sudo apt install certbot python3-certbot-apache
```


Für Nginx:

```
sudo certbot --nginx -d deine-domain.de -d www.deine-domain.de
```
    Ersetze deine-domain.de mit deiner Domain.
    Certbot konfiguriert automatisch Nginx für HTTPS.

Schritt 3: Automatische Erneuerung einrichten

Let’s Encrypt-Zertifikate laufen nach 90 Tagen ab. Certbot erneuert sie automatisch:
bash
```
sudo certbot renew --dry-run  # Testet die Erneuerung
```

    Der Befehl certbot renew läuft automatisch über einen Cron-Job.


#### Wo werden die Zertifikate gespeichert?

    Zertifikat: /etc/letsencrypt/live/deine-domain.de/fullchain.pem
    Privater Schlüssel: /etc/letsencrypt/live/deine-domain.de/privkey.pem

### Wildcard-Zertifikate (für *.deine-domain.de) sind möglich:

```
sudo certbot certonly --manual --preferred-challenges=dns -d *.deine-domain.de
```
✅ Automatische HTTPS-Umleitung in Nginx:

Certbot fügt automatisch eine 301-Weiterleitung von HTTP → HTTPS hinzu.


Zertifikatsstatus prüfen:

```
sudo certbot certificates
## check
dig +short tkk.vpn64.de  # Sollte deine Server-IP zurückgeben
ping tkk.vpn64.de        # Muss erreichbar sein
```
### Nginx-Konfiguration überprüfen

Stelle sicher, dass Nginx die Domain korrekt bedient:
bash
```
sudo nano /etc/nginx/sites-available/tkk.vpn64.de
```
Korrekte Minimal-Konfiguration:
```
nginx

server {
    listen 80;
    server_name tkk.vpn64.de;

    root /var/www/html;
    index index.html;

    location /.well-known/acme-challenge/ {
        allow all;  # Wichtig für Certbot-Validierung!
    }
}
```

    Teste die Konfiguration und starte Nginx neu:
    bash

    sudo nginx -t && sudo systemctl restart nginx

4. Manuelle Validierung erzwingen

Falls Nginx Probleme macht, versuche es mit dem Standalone-Modus (stoppt kurz Nginx):
bash

sudo systemctl stop nginx   # Nginx vorübergehend stoppen
sudo certbot certonly --standalone -d tkk.vpn64.de
sudo systemctl start nginx  # Nginx wieder starten

5. Let’s Encrypt-Rate-Limit umgehen

Falls zu viele Fehlversuche:
bash

sudo certbot delete --cert-name tkk.vpn64.de  # Altes Zertifikat löschen

Warte 1 Stunde, bevor du es erneut versuchst (Let’s Encrypt hat ein Limit von 5 Fehlversuchen pro Stunde).
🌟 Erfolgstest

Nach erfolgreicher Ausstellung:
bash

sudo certbot certificates  # Zeigt das Zertifikat an
curl -I https://tkk.vpn64.de  # Sollte "200 OK" zurückgeben

🔧 Falls immer noch Fehler auftreten:

    Debug-Log prüfen:
    bash

sudo cat /var/log/letsencrypt/letsencrypt.log | grep -i error

Certbot im Debug-Modus ausführen:
bash
```
    sudo certbot --nginx -d tkk.vpn64.de --debug


sudo cat /var/log/letsencrypt/letsencrypt.log | grep -i error
```