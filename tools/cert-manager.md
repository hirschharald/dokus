## Self-Signed Certificates

### Upload existing CA.key and CA.crt files (Option 1)

1. Create a self-signed CA creating a ca.key (private-key) and ca.crt (certificate)

(ca.key)
```bash
openssl genrsa -aes256 -out ca-key.pem 4096
```

(ca.crt)
```bash
openssl req -new -x509 -sha256 -days 3650 -key ca-key.pem -out ca.pem

check with:
openssl x509 -in ca.pem -text 
```
2. self signed certs
```bash
openssl genrsa -out cert-key.pem 4096

openssl req -new -sha256 -subj "/CN=tkk" -key cert-key.pem -out cert.csr

echo "subjectAltName=DNS:*.tkk.de,IP:192.168.10.152" >> extfile.cnf

openssl x509 -req -sha256 -days 365 -in cert.csr -CA ca.pem -CAkey ca-key.pem -out cert.pem CAcreateserial -extfile extfile.cnf

```
