# DemoShopSystemDotnetMicroservicesApp
Demo Shop management system using Microservices with .Net5, DDD programming paradigms, Redis Db, Mongo Db, SQL Db, and services communicate through Docker container
each services follow clean codes rules

# Catalog service use Mongo Database, used for providing products detials
  docker mapping ports into http://localhost:8000

# Basket service use Redis Database, and connect to RabbitMQ as event provider when basket checkout publish
  docker mapping ports into http://localhost:8001

# Order service use SQL server Database, and connect to RabbitMQ as event consumer when basket checkout publish its recived request form RabbitMQ and apply Order from SQL Db
  docker mapping ports into http://localhost:8002
  
# common is class library define connection in RabbitMQ
  RabbitMQ mapping ports into http://localhost:15672  with default username and password
  
# ocelotApiGateway is routing gateway for redirect routing from UI_layer into required services



# for running App in docker container
stop any docker use ports of :  27017 , 15672 , 5672 , 6379 , 8000 , 8002, 8003 , 7000

# build and run command 
  docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up --build
#running and buid in background 
  docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
