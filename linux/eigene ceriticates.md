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

