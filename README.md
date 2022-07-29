# jbm-codetest

###  Users Authentication and Registeration System.

This is originally my solution to a code challenge. It is mostly revolved around common microservices patterns and practices, implemented in .Net 6.0.  
Please find a breif introduction of my implemented solution as follows:

![image](https://user-images.githubusercontent.com/12792659/181851403-3c054f43-ef82-4d06-9a4b-7d8be0758a20.png)

### How to run:
```
docker-compose up -d
```

### Implemented Services:

* Gateway: http://localhost:8050/
* Aggregator API Service: http://localhost:8040/swagger/index.html
* Auth API Service: http://localhost:8020/swagger/index.html
* User API Service: http://localhost:8030/swagger/index.html
* User Worker Service: No url. It is currenlty implemented as an event listener, but can be used to perform regular jobs, ... in future.
  

### Infrastructure Services:

  
* RabbitMQ: http://localhost:5672/
* SQLServer: http://localhost:1433/
* Elasticsearch: http://localhost:9200/
* Kibana: http://localhost:5601/



