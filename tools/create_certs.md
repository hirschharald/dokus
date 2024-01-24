## Self-Signed Certificates

### Upload existing CA.key and CA.crt files (Option 1)

1. Create a self-signed Certificate Authority creating a ca.key (private-key) and ca.crt (certificate)

(ca.key)
```bash
openssl genrsa -aes256 -out ca-key.pem 4096
```

(ca.crt Root cert)
```bash
openssl req -new -x509 -sha256 -days 3650 -key ca-key.pem -out ca-root.pem

Country Name (2 letter code) [AU]:DE
State or Province Name (full name) [Some-State]:BY
Locality Name (eg, city) []:Landshut
Organization Name (eg, company) [Internet Widgits Pty Ltd]:trashserver.net
Organizational Unit Name (eg, section) []:IT
Common Name (eg, YOUR name) []:trashserver.net
Email Address []:sslmaster@domain.com

check with:
openssl x509 -in ca-root.pem -text 
```
2. new self signed certs
```bash
### private key with no password 
openssl genrsa -out cert-key.pem 4096

#### cert request
openssl req -new -sha256 -subj "/CN=tkk" -key cert-key.pem -out cert.csr

echo "subjectAltName=DNS:*.tkk.de,IP:192.168.10.152" >> extfile.cnf

#### create public key cert
openssl x509 -req -sha256 -days 365 -in cert.csr -CA ca-root.pem -CAkey ca-key.pem -out cert-pub.pem CAcreateserial -extfile extfile.cnf


### certs for webserver config
private key of cert
public key of cert --> cert-pub.pem
public key of ca  --> ca-root.pem


Understanding Root Intermediate Server Certificate

    Root Certificate. A root certificate is a digital certificate that belongs to the issuing Certificate Authority. It comes pre-downloaded in most browsers and is stored in what is called a “trust store.” The root certificates are closely guarded by CAs.
    Intermediate Certificate. Intermediate certificates branch off root certificates like branches of trees. They act as middle-men between the protected root certificates and the server certificates issued out to the public. There will always be at least one intermediate certificate in a chain, but there can be more than one.
    Server Certificate. The server certificate is the one issued to the specific domain the user is needing coverage for.

cat cert-pub.pem > fullchain.pem
cat ca-root.pem >> fullchain.pem

```

1. import root certs 
   
Mozilla Firefox verwaltet Zertifikate selbst. Ein neues Zertifikat wird importiert unter “Einstellungen => Erweitert => Zertifikate => Zertifikate anzeigen => Zertifizierungsstellen => Importieren”. Wählt die Datei “ca-root.pem” aus. “Wählt die Option “Dieser CA vertrauen, um Websites zu identifizieren”. 

Chromium / Google Chrome

“Einstellungen” => “Erweiterte Einstellungen anzeigen” (unten) => “HTTPS/SSL” => “Zertifikate verwalten” => “Zertifizierungsstellen” => “Importieren” => “ca-root.pem” auswählen => “Diesem Zertifikat zur Identifizierung von Websites vertrauen”

Debian / Ubuntu
sudo cp ca-root.pem /usr/share/ca-certificates/ca-root.crt
sudo dpkg-reconfigure ca-certificates