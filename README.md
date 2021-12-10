# DemoShopSystemDotnetMicroservicesApp
Demo Shop management system using Microservices with .Net5, DDD programming paradigms, Redis Db, Mongo Db, SQL Db, and services communicate through Docker container
each services follow clean codes rules

# Catalog service use Mongo Database, use for providing products detials
  docker mapping ports into http://localhost:8000
![image](https://user-images.githubusercontent.com/46009744/145629812-5330e784-fbf7-4dba-94c7-677fb2d1fb87.png)

# Basket service use Redis Database, and connect to RabbitMQ as event provider when basket checkout publish
  docker mapping ports into http://localhost:8001
![image](https://user-images.githubusercontent.com/46009744/145629892-e7da4b2c-852c-4e3e-b481-259e11b338f4.png)

# Order service use SQL server Database, and connect to RabbitMQ as event consumer when basket checkout publish its recived request form RabbitMQ and apply Order from SQL Db
  docker mapping ports into http://localhost:8002
![image](https://user-images.githubusercontent.com/46009744/145630011-be30746d-b828-480c-a080-187aa34b0502.png)
  
# common is class library define connection in RabbitMQ
  RabbitMQ mapping ports into http://localhost:15672  with default username and password
  
# ocelotApiGateway is routing gateway for redirect routing from UI_layer into required services



# for running App in docker container
stop any docker use ports of :  27017 , 15672 , 5672 , 6379 , 8000 , 8002, 8003 , 7000

# build and run command 
  docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up --build
#running and buid in background 
  docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d


![image](https://user-images.githubusercontent.com/46009744/145627273-8909e29e-c8da-4277-a651-1d7c442f22ac.png)

![Screenshot (136)](https://user-images.githubusercontent.com/46009744/145627641-f005a3f9-c9fa-48ba-8d79-954e9261cb4b.png)

![image](https://user-images.githubusercontent.com/46009744/145626396-c5c38e47-825f-4975-906d-0e155107bfbd.png)

# UI_layer in Razor pages
![image](https://user-images.githubusercontent.com/46009744/145630152-70d72d1e-cf5e-465f-b57b-55ef44dc412a.png)
![image](https://user-images.githubusercontent.com/46009744/145630233-511809e9-7eed-44e3-bf05-4c752024e525.png)
![image](https://user-images.githubusercontent.com/46009744/145630295-248366d3-4025-420f-94b9-d572ae3fd816.png)

thanks to Mehmet Özkaya tutorials
