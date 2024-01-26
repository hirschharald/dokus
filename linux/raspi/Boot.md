## Boot from ssd

### show Patition UUID
```
blkid
###
/dev/mmcblk0p1: LABEL_FATBOOT="boot" LABEL="boot" UUID="DC3E-E470" TYPE="vfat" PARTUUID="0cdb9d3b-01"
/dev/mmcblk0p2: LABEL="rootfs" UUID="a7adb26a-8b87-4729-99c8-9f5ac069d51e" TYPE="ext4" PARTUUID="0cdb9d3b-02"
/dev/sda1: LABEL_FATBOOT="boot" LABEL="boot" UUID="DC3E-E470" TYPE="vfat" PARTUUID="ea7d0000-01"

```

### Which bootloader Version
```
sudo rpi-eeprom-update
#####
BOOTLOADER: up to date
   CURRENT: Mi 11. Jan 17:40:52 UTC 2023 (1673458852)
    LATEST: Mi 11. Jan 17:40:52 UTC 2023 (1673458852)
   RELEASE: stable (/lib/firmware/raspberrypi/bootloader/stable)
            Use raspi-config to change the release.

  VL805_FW: Using bootloader EEPROM
     VL805: up to date
   CURRENT: 000138c0
    LATEST: 000138c0
#######
```

### change boot order

```
sudo raspi-config
###
6. advanced options
A6 bootorder
```

### clone sd card to ssd

```
dd if=/dev/sdx of=/dev/ssd bs=1M 
```
### expand the filesystem

```
sudo raspi-config
###
6. advanced options
A1 expand file system

### test with
lsblk

```