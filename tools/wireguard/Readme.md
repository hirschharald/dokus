# Fritz!Box WireGuard-Verbindung einrichten

### 1. Fritz!Box Oberfläche aufrufen
- Browser öffnen und `http://fritz.box` oder `192.168.178.1` eingeben
- Mit Fritz!Box Passwort anmelden

### 2. WireGuard-Verbindung einrichten
1. Navigation: `Internet` → `Freigaben` → `VPN`
2. Tab `WireGuard` auswählen
3. Button `WireGuard-Verbindung hinzufügen` klicken

### 3. Konfiguration eintragen
- Name für die Verbindung vergeben (z.B. "WireGuard-VPN")
- WireGuard-Schlüssel:
  - `Schlüsselpaar erzeugen` klicken
  - Den generierten öffentlichen Schlüssel für die Client-Konfiguration notieren
- Netzwerkeinstellungen:
  - IP-Adresse: `10.0.0.1/24` (Beispiel)
  - Port: `51820` (Standardport)

### 4. Client-Zugang konfigurieren
1. Auf `Client hinzufügen` klicken
2. Folgende Daten eintragen:
   - Name des Clients
   - Öffentlichen Schlüssel des Clients einfügen
   - IP-Adresse für den Client festlegen (z.B. `10.0.0.2/32`)

### 5. Berechtigungen einstellen
- Heimnetz-Zugriff aktivieren
- Bei Bedarf: Zugriff auf andere VPN-Teilnehmer erlauben
- Optional: Internet-Zugang über VPN aktivieren

### 6. Port-Freigabe
Die Fritz!Box richtet automatisch die nötige Port-Freigabe ein.

### 7. Verbindungsdaten für Client
Notieren Sie sich für die Client-Konfiguration:
- Öffentliche IP-Adresse der Fritz!Box oder DynDNS-Name
- Port (Standard: 51820)
- Öffentlicher Schlüssel der Fritz!Box

### Sicherheitshinweise:
- Verwenden Sie sichere Schlüssel
- Aktivieren Sie nur benötigte Berechtigungen
- Dokumentieren Sie die Konfiguration
- Speichern Sie die Schlüssel sicher

_Nach der Einrichtung können Sie die WireGuard-Client-Konfiguration mit den notierten Daten erstellen._