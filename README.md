# Order System

## Microservices for Order and Product Management

This project involves the use of microservices for the management of orders and products. 

Each entity is handled by a dedicated service, developed as a [Worker Service](https://learn.microsoft.com/en-us/dotnet/core/extensions/workers?pivots=dotnet-7-0). 

Then, a REST API project is acting as a Gateway, enabling communication between the external world and the services. 

The API and microservices communicate via JSON messages over an [RPC channel using RabbitMQ](https://www.rabbitmq.com/tutorials/tutorial-six-python.html).

## Request flow

1. An external client queries the API.
2. The API receives the request and places it in the appropriate queue managed by RabbitMQ.
3. Each service has a worker listening on its respective queue to receive the request. In services I'm using [Mediatr](https://github.com/jbogard/MediatR) to decouple worker concepts and business logics.
4. The request is sent via Mediatr, which invokes the CommandHandler.
5. The CommandHandler processes the request by interacting with the database through EntityFramework.
6. The CommandHandler returns the response, which then follows the same steps in reverse until it reaches the API.

## Other components

### MessageBroker
It's the shared library where I've implemented the use of RabbitMQ. It exposes an IRPCClient interface and an IRPCServer interface that allow decoupling the other components of the project from RabbitMQ.

### ServiceCommon, ProductCommon, OrderCommon
Contain models, commands, and DTOs shared between the various projects.

## Getting Started

### RabbitMQ

The solution uses RabbitMQ for queue management. There are various solutions possible, I have used the following Docker command to start a pre-configured container:

`docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management`

To open RabbitMQ web console

- Url: http://localhost:15672/#/	
- Username: guest
- Password: guest

### Run 

To test the project you have to run:
- OrderSystemAPI
- OrderService
- ProductService