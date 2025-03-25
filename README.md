# Air Tickets web application

## Description

An SPA application for booking flight tickets

### Backend

REST API implemeted with C# and .NET MVC Framework

MS SQL Server is used as database management system

### Frontend

Client side is built with HTML, CSS, TypeScript and ReactJS.

## How to run

SQL Server can be started either locally or as a Docker container

To run backend type the following command in PowerShell:

```shell
> dotnet run AirTickets.sln
```

To run frontend type the following command in PowerShell:

```shell
> npm start
```

## Extras

Here you can find [API specification](./docs/openapi.yaml) is Swagger format

In directory /docs/ you can find documentation

In directory /scripts/ you can find scripts for creating and filling tables in DB and one script for generating data

In directory /config/ you can find nginx configuration file for this project
