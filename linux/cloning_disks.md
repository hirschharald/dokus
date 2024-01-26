## Cloning disks

### shrink partion

By shrinking the free space of a partition, the required time that dd will take to copy non-used disk blocks, in case of huge partitions, will be drastically reduced.

linux: gparted
windows: diskmgmt.msc 

### clone disks

fdisk -l /dev/sda ----- disk to clone to

```
Festplatte /dev/sda: 238,47 GiB, 256060514304 Bytes, 500118192 Sektoren
Festplattenmodell: USB3.0          
Einheiten: Sektoren von 1 * 512 = 512 Bytes
Sektorgröße (logisch/physikalisch): 512 Bytes / 4096 Bytes
E/A-Größe (minimal/optimal): 4096 Bytes / 4096 Bytes
Festplattenbezeichnungstyp: dos
Festplattenbezeichner: 0xea7d0000
``` 

fdisk -l /dev/sda ---- disk for cloning

``` 
Festplattenmodell: STORAGE DEVICE  
Einheiten: Sektoren von 1 * 512 = 512 Bytes
Sektorgröße (logisch/physikalisch): 512 Bytes / 512 Bytes
E/A-Größe (minimal/optimal): 512 Bytes / 512 Bytes
Festplattenbezeichnungstyp: dos
Festplattenbezeichner: 0x0cdb9d3b

Gerät      Boot Anfang     Ende Sektoren Größe Kn Typ
/dev/sdb1         8192   532479   524288  256M  c W95 FAT32 (LBA)
/dev/sdb2       532480 60551167 60018688 28,6G 83 Linux

0
sudo apt install pv

sudo dd if=/dev/sdb bs=512 count=60551167 conv=sync,noerror | pv |sudo dd of=/dev/sda

```


