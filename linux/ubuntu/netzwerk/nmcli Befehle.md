### Commands
```

nmcli connection show

sudo nmcli connection modify Kabel ipv4.addresses "192.168.1.152/24"
sudo nmcli connection modify Kabel ipv4.gateway "192.168.10.11"
sudo nmcli connection modify Kabel ipv4.dns "192.168.10.152,102.168.10.11,8.8.8.8,8.8.4.4"
sudo nmcli connection modify Kabel ipv4.method manual
```
```


sudo nmcli connection down Kabel
sudo nmcli connection up Kabel
```


`nmcli` ist ein Kommandozeilenwerkzeug zur Verwaltung von Netzwerkverbindungen in Linux-Systemen, die NetworkManager verwenden. Hier sind einige grundlegende `nmcli`-Befehle, die Ihnen helfen können, Netzwerkverbindungen zu verwalten:

1. **Netzwerkstatus anzeigen:**
   ```bash
   nmcli general status
   ```

2. **Verfügbare Netzwerkverbindungen auflisten:**
   ```bash
   nmcli connection show
   ```

3. **Aktive Verbindungen anzeigen:**
   ```bash
   nmcli connection show --active
   ```

4. **Neue Verbindung erstellen:**
   ```bash
   nmcli connection add type <type> con-name <name> ifname <interface>
   ```
   Ersetzen Sie `<type>`, `<name>` und `<interface>` durch die entsprechenden Werte (z. B. `wifi`, `my_wifi`, `wlan0`).

5. **Verbindung aktivieren:**
   ```bash
   nmcli connection up <name>
   ```

6. **Verbindung deaktivieren:**
   ```bash
   nmcli connection down <name>
   ```

7. **Verbindung löschen:**
   ```bash
   nmcli connection delete <name>
   ```

8. **IP-Adresse und andere Details einer Verbindung anzeigen:**
   ```bash
   nmcli device show <interface>
   ```

9. **Geräte anzeigen:**
   ```bash
   nmcli device
   ```

10. **Wi-Fi-Netzwerke scannen:**
    ```bash
    nmcli device wifi list
    ```

Diese Befehle sind nur ein Ausgangspunkt. `nmcli` bietet viele weitere Optionen und Parameter,
 die Sie je nach Bedarf verwenden können. Um mehr über einen bestimmten Befehl zu erfahren, 
 können Sie `nmcli help` oder `man nmcli` verwenden.




 #### Verbindung editieren 
        nmcli connection edit Kabel
        set ipv4.dns-search domain.local