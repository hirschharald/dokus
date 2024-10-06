## install docker

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




curl -fsSL https://download.docker.com/linux/raspbian/gpg | sudo tee /etc/apt/trusted.gpg.d/docker.asc > /dev/null

3a add ocker to the sources list

echo "deb [arch=armhf signed-by=/etc/apt/trusted.gpg.d/docker.asc] https://download.docker.com/linux/raspbian $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

3b 
sudo apt update


4. installeire docker engine

sudo apt-get install -y docker-ce


5. Überprüfe, ob Docker erfolgreich installiert wurde:

sudo docker run hello-world
```

### install docker-compose

```
1. aktuelle version
   
   sudo curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

2.

   sudo chmod +x /usr/local/bin/docker-compose

3. überprüfe
   
   docker-compose --version
```

### docker als user ausführen

```
sudo usermod -aG docker $USER
```