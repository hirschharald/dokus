
````
fdisk -l /path/to/img/arch.img
````

Disk arch.img: 15.1 GiB, 16172187648 bytes, 31586304 sectors

Units: sectors of 1 * 512 = 512 bytes

Sector size (logical/physical): 512 bytes / 512 bytes

I/O size (minimum/optimal): 512 bytes / 512 bytes

Disklabel type: dos

Disk identifier: 0x00057540



Device    Boot     Start       End   Blocks  Id System

arch.img1           2048    186367    92160   c W95 FAT32 (LBA)

arch.img2         186368  31586303 15699968   5 Extended

arch.img5         188416  31584255 15697920  83 Linux

````
mkdir /mnt/sd1

mkdir /mnt/sd2

mount -t auto -o loop,offset=$((2048*512)) /path/to/img/arch.img /mnt/sd1

mount -t auto -o loop,offset=$((188416*512)) /path/to/img/arch.img /mnt/sd2
````