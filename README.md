# WebAPIDotNetCore

Exemplo de api em dot net core que consome uma api externa.

## Tecnologias utilizadas

* AspNet Core WebAPI
* Entity Framework Core
* SQLLite
* Docker

## Como Executar com Docker?

```bash
docker build -t api -f WebAPIDotNetCore.Api/Dockerfile .
docker run -d -p 5000:5000 --name api api
```

Abrir [URL](http://localhost:5000/swagger)
