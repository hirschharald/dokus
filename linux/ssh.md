

 

## Standardmäßig erfolgt der Login via SSH auf einem Server mit Benutzername und Passwort. Neben dieser Art der Authentifizierung unterstützt SSH außerdem die Authentifizierung mittels Public-/Private-Key Verfahrens. Dieses gilt im Gegensatz zur Passwort-Authentifizierung als wesentlich sicherer, da ein Hack aufgrund eines unsicheren Kennworts nicht mehr möglich ist. Sinnvollerweise wird daher nach der einrichtung die Passwort-Authentifizierung deaktiviert, es ist jedoch auch möglich beide Varianten parallel zu nutzen.

### Zuerst erstellen wir mit openssh das benötigte Schlüsselpaar. In diesem Fall einen RSA-Schlüssel mit einer Schlüssellänge von 4096 Bit mit dem Befehl
ssh-keygen -b 4096

```
ssh-keygen -b 4096
```
#### Generating public/private rsa key pair.Enter file in which to save the key (/home/optimox/.ssh/id_rsa):
Enter passphrase (empty for no passphrase):
Enter same passphrase again:
Your identification has been saved in /home/username/.ssh/id_rsa.
Your public key has been saved in /home/username/.ssh/id_rsa.pub.
The key fingerprint is:
7d:f5:71:2e:f0:dc:f1:a2:e7:60:07:37:69:b3:ce:31 username@client
The key's randomart image is:
+--[ RSA 4096]----+
|                 |
|                 |
|            . .o.|
|         .   = ==|
|        S . o @ =|
|           . = * |
|            + E  |
|           . B o |
|              +  |
+-----------------+

Die Frage wo der Key gespeichert werden soll kann getrost mit Return übersprungen werden. Anschließend folgt die Frage nach dem Passwort für den Schlüssel. Wenn kein Passwort eingegeben wird liegt der Schlüssel im Klartext auf der Festplatte. Ohne Passwortschutz kann sich jeder, der im Besitz des Schlüssels ist auf dem Server einloggen. Sinnvollerweise wird der Schlüssel selbst also ebenfalls mit einem Passwort verschlüsselt. Sollte der Schlüssel gestohlen werden benötigt der Angreifer zusätzlich zum Schlüssel noch das Passwort.

#### Nun muss der öffentliche Schlüssel noch auf den Server übertragen werden mit dem Befehl ssh-copy-id -i .ssh/id_rsa.pub username@ipadresse

```
 ssh-copy-id -i .ssh/id_rsa.pub optimox@192.168.30.118

```
The authenticity of host '192.168.178.118 (192.168.178.118)' can't be established.
ECDSA key fingerprint is 71:ab:21:c8:20:66:8c:4d:b9:b2:6b:0d:62:29:aa:de.
Are you sure you want to continue connecting (yes/no)? yes
/usr/bin/ssh-copy-id: INFO: attempting to log in with the new key(s), to filter out any that are already installed
/usr/bin/ssh-copy-id: INFO: 1 key(s) remain to be installed -- if you are prompted now it is to install the new keys
username@192.168.178.118's password:

Number of key(s) added: 1

#### Now try logging into the machine, with:  
 ```
 ssh 'username@192.168.178.118'

 ```
and check to make sure that only the key(s) you wanted were added.

Damit wurde auf dem Server die Datei /home/username/uthorized_keys erstellt, die jetzt folgendermaßen aussieht:

```
cat .ssh/authorized_keys
```
ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQCkEZvIdJ1c12QzyhgEw8/S6cwF8RVAAG1XwIgO2712Mijv47FOLQ5S3FZdLuICHaUUexnP3RK/nZJ2hh2tocmUGyqq4I3pW3Mup7NYzMeaTDWgj26Bipn3mHi+ixtKC3X6gEUk3GFcKFT1U9sTKFQDzXD6Stq7awFXBmvO74VGwSKJIdc1OFOpEqEsSvC/kSfVEoSrm1BTkWdDZN6KNg0XwJ+wa/Lk0NvihUXBKCP2B6YDVv5GmmfVmMKbpDZUFMP5rkKB4cA7ggSzWZGQEtDYErPBRlulWYCF4ZEZ/QK1QcwIhI/wE99W2hy+jlEYKL7kRjszo/85PJk4qiqw8iGHjUPHJdDHgN8ravPS/PZKs/xYoja518cYtukwIeZyWCsLsDJ276Weygou6BdocSEB5u2VC/dg6DO+WLDbxL0tX1/ZbAqoQxe186esf/1Cj9wuVcWD8U5Ly7/hIA2T7ZZLdIa4oZEe23bq14PxG/aE+soS9g3mFAPdkuPaACl91Bki1MUfVQ8+UeHVCVKb0fG5stjht9+z8dtBTgt6ZSByqRyaqQ6B3OkUWFCmcCoBZdDZ+SKdWSLOsdeBY4svlWIZQvP5rXV9ZkMCEt/4obcvN3R8rX/eeg02P2zspnNOP2r2vDc11+geE2SzplUrZsWTg8QHtW8odMpAkC2drn7WlQ== username@client

Sollte der Schlüssel einmal verloren gehen, kann dieser Eintrag entfernt werden, wodurch der verlorene Schlüssel auch nicht mehr vom Server akzeptiert wird.

Nun kann die Key-Authentifizierung getestet werden mit:

```
ssh  -i .ssh/id_rsa username@192.168.178.118
```

Wenn bei der Schlüsselerstellung ein Passwort angegeben wurde wird dieses jetzt abgefragt. Anschließend ist man auf dem entferntet System eingeloggt

```
ssh -i .ssh/id_rsa username@192.168.178.118
```

Nachdem dieser Test erfolgreich war kann der Login mit Passwort deaktiviert werden. Dazu muss die Datei /etc/ssh/sshd_config angepasst werden.
```
 nano /etc/ssh/sshd_config
```

Hier wird die Einstellung PasswordAuthentication yes zu PasswordAuthentication no geändert.

nun muss der SSH Dienst auf dem Server noch neu gestartet werden

# sudo service ssh restart

Ein erneuter Test zeigt, dass nun der Login ohne Schlüssel nicht mehr möglich ist. Dementsprechend wichtig ist ein Backup der Schlüsseldateien.

```
#ssh -i .ssh/id_rsa username@192.168.178.118
Agent admitted failure to sign using the key.
Permission denied (publickey)

```


    Mein Vorschlag:
    Lassse dein Bash-Script alle Host-s einlesen und durchlaufe diese:
    
    server=“$(cat ~/.ssh/config | grep -E „^Host “ | cut -d ‚ ‚ -f2″

    for host in $server; do
    #Ersetze Keys
    #dazu musst du den zugeordneten Key ermitteln und den User auf dem der Public liegt.
    #Ersetze für $user auf $host den alten Pubkey-String $oldkey durch deinen neuen Pubkey-String $newkey
    ssh $user@$host „sed -i \“s/$oldkey/$newkey/g“
    done

    Falls du alle durch den selben ersetzten willst kannst du dann auch deine ssh-config mit einmal abändern,
    indem du alle Zeilen, die IdentityFile enthalten durch eine neue Zeile ersetzt


## Neues Passwort
```
ssh-keygen -p -f ./id_rsa
   