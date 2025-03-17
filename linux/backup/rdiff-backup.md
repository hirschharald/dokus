


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
