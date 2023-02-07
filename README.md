# dotnet-smtp-microservice

.NET REST API microservice for sending emails through SMTP. Uses [MailHog](https://github.com/mailhog/MailHog) as a local SMTP server.

## Prerequisites

- [Docker](https://www.docker.com/)

## Local development

Steps to develop this microservice locally (after cloning repo and starting Docker):

1. `docker-compose -f dev-stack.yaml` This will start MailHog (SMTP + Web)
2. `dotnet run --project Microservice.Api` Start the .NET API
3. Open up MailHog's Web UI at [http://localhost:8001](http://localhost:8001)
4. Open up .NET API Swagger at [https://localhost:8002/swagger](https://localhost:8002/swagger)
5. Perform a `/send` action and see MailHog's Web UI!

![image](https://user-images.githubusercontent.com/29711974/217379467-1121032f-bd5a-4e9a-b02d-a24b1f2a93e6.png)

## Tests

Using [Testcontainers](https://github.com/testcontainers/testcontainers-dotnet) to spin up a MailHog container on demand. To retrieve sent messages, querying [MailHog's API](https://github.com/mailhog/MailHog/blob/master/docs/APIv2.md). Running tests require Docker.
