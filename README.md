# dotnet-smtp-microservice

.NET REST API microservice for sending emails through SMTP. Uses [MailHog](https://github.com/mailhog/MailHog) as a local SMTP server.

## Prerequisites

- [Docker](https://www.docker.com/)

## Local development

Steps to develop this microservice locally:

1. Clone the repo
2. `docker-compose -f dev-stack.yaml` This will start MailHog (SMTP+UI)
3. `dotnet run --project Microservice.Api` Start the .NET API
4. Open up MailHog's UI at [http://localhost:8001](http://localhost:8001)
5. Open up .NET API Swagger at [https://localhost:8002/swagger](https://localhost:8002/swagger)
6. Perform a `/send` action and see MailHog's UI!

![image](https://user-images.githubusercontent.com/29711974/217379467-1121032f-bd5a-4e9a-b02d-a24b1f2a93e6.png)
