


### Backup
```
rdiff-backup backup foo bar
rdiff-backup backup /some/local-dir hostname.net::/whatever/remote-dir
```


#### exclude
```
rdiff-backup backup --exclude /tmp --exclude /mnt --exclude /proc user@host.net::/ /backup/host.net
```

### Restore



Suppose earlier we have run rdiff-backup foo bar, with both foo and bar local. We accidentally deleted foo/dir and now want to restore it from bar/dir.
```
cp -a bar/dir foo/dir
```

```
rdiff-backup --restore-as-of now host.net::/remote-dir/file local-dir/file
```




But the main advantage of rdiff-backup is that it keeps version history. This command restores host.net::/remote-dir/file as it was 10 days ago into a new location /tmp/file.
```
rdiff-backup -r 10D host.net::/remote-dir/file /tmp/file
```
### Delete

```
rdiff-backup remove increments --older-than 2W host.net::/remote-dir
```

### Test Remote
```
rdiff-backup test hostname.net::/somedir
```
## Beispiele für Backup
```

rdiff-backup -v5 backup --exclude-filelist .excludeBackup /home/tkk /media/tkk/backup_daten/tkk
rdiff-backup -v5 backup --exclude-filelist .excludeDatenBackup --exclude-regexp '.*/node_modules' /media/tkk/Daten /media/tkk/backup_daten/daten

```
## Server per ssh sichern

```
rdiff-backup root@IP-Adresse::QUELL-VERZEICHNIS BACKUP-VERZEICHNIS
```

Ubuntu-Benutzer werden mit dem Problem konfrontiert, dass sich root per default nicht mit einem Passwort über SSH anmelden darf. Um trotzdem Daten zu sichern, auf die am Server nur root zugreifen darf, wird ein Umweg über sudo eingerichtet:[1]
```
$ sudo vi /etc/sudoers.d/rdiff
tktest  ALL=(root)NOPASSWD:/usr/bin/rdiff-backup
```
Achtung: Nach dem Erstellen dieser Konfiguration darf der User tktest /usr/bin/rdiff-backup mit sudo ohne Eingabe eines Passwortes ausführen! Am besten wird zum Ausführen der Backups ein eigener User erstellt und die Kommandos eingeschränkt, die der User über SSH ausführen darf:

### Ausführbare SSH-Kommandos per authorized keys einschränken

Um den Benutzer auf ein einzelnes Kommando einzuschränken, wird der Parameter command= vor den Schlüssel eingetragen. Danach wird beim Versuch des Aufbaus einer SSH-Verbindung immer nur dieses Kommando ausgeführt, auch wenn z.B. ein anderes Kommando übergeben wurde.[1] Im folgenden Beispiel wird der Benutzer dailybackup zu Demonstrationszwecken auf das Kommando date eingeschränkt. Dazu wird am SSH-Server der Paramater command=date definiert:

:~$ cat .ssh/authorized_keys 
command="rdiff_backup" ssh-rsa AAAA[...]

-------------------------------------------------------------------------------------------








Beim Aufruf von rdiff-backup muss dann sicher gestellt werden, dass am Server sudo rdiff-backup aufgerufen wird:

$ rdiff-backup --remote-schema 'ssh -C %s sudo rdiff-backup --server' tktest@192.168.56.105::/etc backup/

Das default Remote-Schema kann in der man Page von rdiff-backup nachgelesen werden, im obigen Beispiel wird einfach sudo ergänzt. 