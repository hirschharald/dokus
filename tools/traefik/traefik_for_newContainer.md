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