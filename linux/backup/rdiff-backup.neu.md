# rdiff-backup Kommando-Referenz
# ==============================

# Grundlegende Backup-Befehle
# ---------------------------

# Lokales Backup erstellen
rdiff-backup /quellverzeichnis /zielverzeichnis

# Remote-Backup über SSH
rdiff-backup /lokales/verzeichnis benutzer@hostname:/remote/backup
rdiff-backup benutzer@quellhost:/remote/quelle /lokales/backup
rdiff-backup benutzer1@quellhost:/remote/quelle benutzer2@zielhost:/remote/backup

# Backup mit verschiedenen Verbose-Leveln
rdiff-backup -v 0 /quelle /ziel        # Still mode
rdiff-backup -v 1 /quelle /ziel        # Fehler anzeigen
rdiff-backup -v 2 /quelle /ziel        # Warnungen anzeigen
rdiff-backup -v 3 /quelle /ziel        # Info anzeigen
rdiff-backup -v 5 /quelle /ziel        # Debug modus
rdiff-backup -v 8 /quelle /ziel        # Sehr ausführlich
rdiff-backup -v 9 /quelle /ziel        # Extrem ausführlich

# Backup mit Statistiken
rdiff-backup --print-statistics /quelle /ziel

# Backup-Verwaltung und Wiederherstellung
# ---------------------------------------

# Verfügbare Increments auflisten
rdiff-backup --list-increments /backup-verzeichnis
rdiff-backup --list-increment-sizes /backup-verzeichnis

# Zu bestimmten Zeitpunkt wiederherstellen
rdiff-backup -r now /backup /wiederherstellung
rdiff-backup -r 1D /backup /wiederherstellung        # 1 Tag alt
rdiff-backup -r 1B /backup /wiederherstellung        # 1 Backup alt
rdiff-backup -r 2W /backup /wiederherstellung        # 2 Wochen alt
rdord-backup -r 1M /backup /wiederherstellung        # 1 Monat alt
rdiff-backup -r 2024-01-15T10:30:00 /backup /wiederherstellung

# Bestimmte Datei/Directory wiederherstellen
rdiff-backup -r 1D /backup/pfad/zur/datei /ziel/pfad

# Backup-Bereinigung
# ------------------

# Alte Backups entfernen
rdiff-backup --remove-older-than 7D /backup          # Älter als 7 Tage
rdiff-backup --remove-older-than 1M /backup          # Älter als 1 Monat
rdiff-backup --remove-older-than 6M /backup          # Älter als 6 Monate
rdiff-backup --remove-older-than 1Y /backup          # Älter als 1 Jahr

# Force entfernen
rdiff-backup --force --remove-older-than 30D /backup

# Ausschluss-Optionen
# -------------------

# Einzelne Ausschlüsse
rdiff-backup --exclude "*.tmp" /quelle /ziel
rdiff-backup --exclude "/home/*/Downloads" /quelle /ziel
rdiff-backup --exclude "**/cache" /quelle /ziel

# Mehrfache Ausschlüsse
rdiff-backup --exclude "*.log" --exclude "*.tmp" --exclude "/tmp" /quelle /ziel

# Ausschluss-Liste aus Datei
rdiff-backup --exclude-filelist /pfad/ausschlussliste.txt /quelle /ziel

# Include bestimmter Pfade (trotz Ausschluss)
rdiff-backup --include "/wichtig" --exclude "**" /quelle /ziel

# Glob-Muster für Ausschlüsse
rdiff-backup --exclude-globbing-filelist /pfad/globbing-list.txt /quelle /ziel

# Erweiterte Optionen
# -------------------

# Maximale Dateigröße setzen
rdiff-backup --max-file-size 100M /quelle /ziel

# SSH-Optionen
rdiff-backup --remote-schema "ssh -C -p 2222 %s rdiff-backup --server" /quelle user@host:/backup

# Bandbreite begrenzen
rdiff-backup --remote-schema "ssh -o ConnectTimeout=60 %s rdiff-backup --server" /quelle /ziel

# Backup testen ohne zu schreiben
rdiff-backup --dry-run /quelle /ziel

# Force Operationen
rdiff-backup --force /quelle /ziel

# Create full verbindung (für initiale Backups)
rdiff-backup --create-full-path /quelle /ziel

# Vergleich und Überprüfung
# -------------------------

# Unterschiede zwischen Quelle und Backup anzeigen
rdiff-backup --compare /quelle /backup

# Vergleich mit bestimmten Increment
rdiff-backup --compare-at-time 1D /quelle /backup

# Metadaten-Operationen
# ---------------------

# ACLs erhalten
rdiff-backup --preserve-numerical-ids /quelle /ziel

# Besitzer/Gruppen erhalten
rdiff-backup --no-file-statistics /quelle /ziel

# Server-Kommandos
# ----------------

# Server-Modus starten
rdiff-backup --server

# Test-Kommandos
# --------------

# Version anzeigen
rdiff-backup --version

# Hilfe anzeigen
rdiff-backup --help

# Konfigurationsdatei verwenden
rdiff-backup --parsable-output /quelle /ziel

# Beispiel für komplexes Backup-Setup
# ------------------------------------

# Komplexes Backup mit Ausschlüssen und Limits
rdiff-backup \
  --exclude "**/tmp" \
  --exclude "**/cache" \
  --exclude "*.log" \
  --exclude "*.tmp" \
  --max-file-size 1G \
  --remote-schema "ssh -C -c aes256-ctr %s rdiff-backup --server" \
  -v 3 \
  --print-statistics \
  /wichtige/daten \
  backup-user@backup-server:/backups/hauptserver

# Cron-Job für automatische Backups
# 0 2 * * * rdiff-backup --remove-older-than 30D /backup/hauptserver
# 0 3 * * * rdiff-backup --exclude-filelist /etc/backup-excludes.txt /daten backup-user@server:/backups

# Beispiel für Wiederherstellungsszenario
# ----------------------------------------

# 1. Verfügbare Backups anzeigen
rdiff-backup --list-increments /backups/hauptserver

# 2. Komplettes System vom gestrigen Stand wiederherstellen
rdiff-backup -r 1D /backups/hauptserver /wiederherstellung/bereich

# 3. Nur bestimmten Ordner wiederherstellen
rdiff-backup -r 2D /backups/hauptserver/home/user/dokumente /tmp/wiederhergestellt

# 4. Alte Backups bereinigen
rdiff-backup --remove-older-than 90D /backups/hauptserver