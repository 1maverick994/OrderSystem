Starts RabbitMQ in a Docker container 

`docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management`

To open RabbitMQ web console
---------------------------------------
|Url:		|http://localhost:15672/#/	|
|Username:	| guest						|
|Password:	| guest						|