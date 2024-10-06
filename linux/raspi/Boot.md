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

### install docker

```
sudo apt-get update

2. installiere Pakete

sudo apt-get install -y \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common

3.Füge das offizielle Docker-Repository hinzu:

curl -fsSL https://download.docker.com/linux/debian/gpg | sudo apt-key add -
sudo add-apt-repository \
   "deb [arch=arm64] https://download.docker.com/linux/debian \
   $(lsb_release -cs) \
   stable"

4. installeire docker engine

sudo apt-get install -y docker-ce docker-ce-cli containerd.io


5. Überprüfe, ob Docker erfolgreich installiert wurde:

sudo docker run hello-world


### install docker-compose

```
1. aktuelle version
   
   sudo curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

2.

   sudo chmod +x /usr/local/bin/docker-compose

3. überprüfe
   
   docker-compose --version
