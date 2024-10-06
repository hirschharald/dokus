### mit docker


/path/to/your/project/
│
├── docker-compose.yml
├── volumes/nginx/nginx.conf.d
└── volumes/vw-data/  # Dieses Verzeichnis wird für Vaultwarden-Daten verwendet


#### überprüfen wer welche ports belegt
```
netstat -tlnp | grep 80
```

### docker compose
```
version: '3'

services:

  vaultwarden:
    image: vaultwarden/server:latest
    container_name: vaultwarden
    restart: unless-stopped

    ports:
      - "8080:80" # Port für Vaultwarden
    environment:
      - ADMIN_TOKEN=your-admin-token
      - DATABASE_URL=volumes/vw-data/db.sqlite3
      - DOMAIN=https://vaultwarden.example.com
      - SMTP_HOST=smtp.example.com
      - SMTP_PORT=587
      - SMTP_FROM=your_email@example.com
      - SMTP_USERNAME=your_email@example.com
      - SMTP_PASSWORD=your_password
      - SIGNUPS_ALLOWED=false
      - WEBSOCKET_ENABLED=true
    volumes:
      - .volumes/vw-data:/data

  nginx:
    image: nginx:alpine
    container_name: nginx
    restart: unless-stopped
    ports:
      - 80:80
      - 443:443
    volumes:
      - .volumes/nginx/conf.d:/etc/nginx/conf.d
      - .volumes/nginx/ssl:/etc/nginx/ssl
    depends_on:
      - vaultwarden

```
### erstelle verzeichnisse für nginx und vw
```
mkdir -p volumes/nginx/conf.d volumes/nginx/ssl volumes/vw-data

### Erstelle die Nginx-Konfigurationsdatei nginx/conf.d/vaultwarden.conf
```
### nginx Konfig

```
server {
    listen 80;
    listen 443 ssl;

    # Server-Name
    server_name vaultwarden.example.com;

    # SSL-Konfiguration
    ssl_certificate /etc/nginx/ssl/crt.pem;
    ssl_certificate_key /etc/nginx/ssl/key.pem;
    ssl_protocols TLSv1.2 TLSv1.3;
    
    ssl_ciphers ECDHE-ECDSA-AES256-GCM-SHA384:ECDHE-RSA-AES256-GCM-SHA384:ECDHE-ECDSA-CHACHA20-POLY1305:ECDHE-RSA-CHACHA20-POLY1305:ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-SHA384:ECDHE-RSA-AES256-SHA384:ECDHE-ECDSA-AES128-SHA256:ECDHE-RSA-AES128-SHA256;
    ssl_prefer_server_ciphers on;


    # Weiterleitung von HTTP zu HTTPS
    if ($scheme = http) {
        return 301 https://$server_name$request_uri;
    }

    # Proxy-Konfiguration für Vaultwarden
    location / {
        proxy_pass http://vaultwarden:8080;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
    # proxy for api  
    location /api {
      proxy_buffering off;
      proxy_set_header X-Forwarded-Proto $scheme;
      proxy_set_header X-Forwarded-Host $host;
      proxy_set_header X-Forwarded-Port $server_port;

      proxy_pass http://backend:4000/api;
    }
}

```