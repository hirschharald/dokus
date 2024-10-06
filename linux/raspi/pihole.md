### Pihole mit fritz box

1. feste IP on fritz


2. sudo -s

3. apt update


### firewall inst
    apt install ufw
    systemctl enable ufw --now
    ufw status
    port 22 freigeben
    ufw allow from 192.168.10.0/24 to any port 22
    ufw allow 80,443,53,853/tcp
    ufw allow 53,853/udp

    ########## Achtung ##############\
    ufw enable

    ufw status verbose

### pihole inst

### ihole konfig

    DNS Konfig

    --> settings --> advanced dns --> use dnssec


### fritz konfig

    heimnetz-->netztwerk-->   netzwerkeinst -->ipv4 --> lokalerdns
   
