# Sample House Info - Containerized API with Identity Server 6

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![Nginx](https://img.shields.io/badge/nginx-%23009639.svg?style=for-the-badge&logo=nginx&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)

# Description

This .NET Core solution demonstrates a clean architectured WebAPI that is protected with IdentityServer service and consumed by three different clients:
 1. Console App, uses client credential flow.
 2. Server-side ASP.NET Core web application, uses authorization code flow.
 3. SPA ASP.NET Core Blazor WASM, uses PKCE flow

# Setup

1. Generate a self-signed certificate with a new private key.
    ```shell
    openssl req -x509 -newkey rsa:4096 -keyout localhost.key -out localhost.crt -subj "/CN=localhost" -addext "subjectAltName=DNS:localhost,DNS:api,DNS:identityserver,DNS:singlepageapplication,DNS:webapplication"
    ```
    ```shell
    openssl pkcs12 -export -in localhost.crt -inkey localhost.key -out localhost.pfx -name "Creating an IdentityServer 6 Solution"
    ```
2. Import the self-signed certificate.
    ```shell
    certutil -f -user -importpfx Root localhost.pfx
    ```
3. Add the line below to the hosts file.
    ```text
    127.0.0.1 api
    127.0.0.1 identityserver
    127.0.0.1 singlepageapplication
    127.0.0.1 webapplication
    ```
4. Run the service containers.
    ```shell
    docker compose up --build
    ```

- The output of the console client can be invistigated by checking the container logs.
- Go to https://webapplication:7002/ for the server-side web client.
- Go to http://singlepageapplication:7003/ for the Blazor WASM client.
