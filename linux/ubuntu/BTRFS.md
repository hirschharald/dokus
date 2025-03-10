
# BTRFS Filesystem
Hier sind einige wichtige Befehle für die Arbeit mit Btrfs, die dir helfen können, das Dateisystem zu verwalten und zu nutzen:
### Btrfs-Installation

Stelle sicher, dass die Btrfs-Tools installiert sind:


```
sudo apt install btrfs-progs
```
### Btrfs-Partition erstellen

Um eine neue Btrfs-Partition zu erstellen, kannst du den mkfs.btrfs-Befehl verwenden:


```
sudo mkfs.btrfs /dev/sdXn
```
Ersetze /dev/sdXn durch den Gerätenamen deiner Partition.

### Btrfs-Dateisystem anzeigen

Um Informationen über das Btrfs-Dateisystem anzuzeigen:


```
sudo btrfs filesystem show
```
### Btrfs-Subvolumes anzeigen

Um alle Subvolumes in einem Btrfs-Dateisystem anzuzeigen:


```
sudo btrfs subvolume list /mountpoint
```
Ersetze /mountpoint durch den Pfad zu deinem Btrfs-Mountpunkt.
### Btrfs-Subvolume erstellen

Um ein neues Subvolume zu erstellen:


```
sudo btrfs subvolume create /mountpoint/subvolume_name
```
### Btrfs-Subvolume löschen

Um ein Subvolume zu löschen:


```
sudo btrfs subvolume delete /mountpoint/subvolume_name
```
### Btrfs-Snapshots erstellen

Um einen Snapshot eines Subvolumes zu erstellen:


```
sudo btrfs subvolume snapshot /mountpoint/source_subvolume /mountpoint/snapshot_name
```
### Btrfs-Snapshots anzeigen

Um alle Snapshots anzuzeigen:


```
sudo btrfs subvolume list /mountpoint
```
### Btrfs-Disknutzung anzeigen

Um die Nutzung des Btrfs-Dateisystems anzuzeigen:


```
sudo btrfs filesystem df /mountpoint
```
### Btrfs-Integritätsprüfung

Um die Integrität des Btrfs-Dateisystems zu überprüfen:


```
sudo btrfs scrub start /mountpoint
```
Um den Status des Scrubs zu überprüfen:


```
sudo btrfs scrub status /mountpoint
```
### Btrfs-Fehlerprotokoll

Um das Fehlerprotokoll anzuzeigen:


```
sudo btrfs device stats /mountpoint
```
### Btrfs-Geräte hinzufügen

Um ein neues Gerät zu einem Btrfs-Dateisystem hinzuzufügen:


```
sudo btrfs device add /dev/sdXn /mountpoint
```
### Btrfs-Geräte entfernen

Um ein Gerät aus einem Btrfs-Dateisystem zu entfernen:


```
sudo btrfs device remove /dev/sdXn /mountpoint
```
### Btrfs-Umount

Um ein Btrfs-Dateisystem zu unmounten:


```
sudo umount /mountpoint
```
### Btrfs-Mount

Um ein Btrfs-Dateisystem zu mounten:


```
sudo mount -o subvol=subvolume_name /dev/sdXn /mountpoint
```
