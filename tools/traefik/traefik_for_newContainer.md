####   Labels

```  
#### sample for nginx
traefik.enable 	                                true
traefik.http.routers.nginx.entrypoints 	        websecure
traefik.http.routers.nginx.rule 	            Host("192.168.10.152")
traefik.http.routers.nginx.tls 	                true
traefik.http.routers.nginx.tls.certresolver 	staging     #### name of traefik section

```

```  
#### sample for vaultwarden
traefik.enable 	                                        true
traefik.http.routers.vaultwarden.entrypoints 	        websecure
traefik.http.routers.vaultwarden.rule 	                Host("192.168.10.152") && Path("/vaultwarden")
traefik.http.routers.vaultwarden.tls 	                true
traefik.http.routers.vaultwarden.tls.certresolver 	    staging     #### name of traefik section

```

```
labels:
  - traefik.enable                                              true
  - traefik.docker.network                                      traefik
  - traefik.http.middlewares.redirect-https.redirectScheme.scheme                                                        https
  - traefik.http.middlewares.redirect-https.redirectScheme.permanent      true
  
  - traefik.http.routers.bitwarden-http.rule                    Host(`bitwarden.domain.tld`)
  - traefik.http.routers.bitwarden-http.entrypoints             web
  - traefik.http.routers.bitwarden-http.middlewares             redirect-https

  - traefik.http.routers.bitwarden-https.rule                   Host(`bitwarden.domain.tld`)
  - traefik.http.routers.bitwarden-https.entrypoints            websecure
  - traefik.http.routers.bitwarden-https.tls                    true
  - traefik.http.routers.bitwarden-https.service                bitwarden


  - traefik.http.routers.bitwarden-http.service                 bitwarden
  - traefik.http.services.bitwarden.loadbalancer.server.port    80

```