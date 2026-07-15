# OrderFlow
This project aims to study the production environment and techniques to develop a .NET web api using ASP.NET, .NET EF, databases, testing, ci/cd and other important steps on the flow to create a comprehensive application for study.

## Planning and Goals
### Goal
Create an API for a simplified store in which you can manage products, stock, customers e orders, it must also contain business logic.

### Planing
#### Architecture
Due to it's ample use in market practices i've chosen to apply a clean layered architecture. In which business logic must not depend on technical details such as database, web framework and others.

Therefore, the following division was chosen:
- OrderFlow.Domain : entities, business logic and repository interface
- OrderFlow.Application : User Cases, DTOs, validation, servie interface
- OrderFlow.Infrastructure : EF Core, repository implementation, external services
- OrderFlow.API : Controllers, middlewares, settings

It's important to notice the dependancies between these layers: API is dependent on Application, Application is dependent on Domain, Infrastructure is dependent on both Domain and Application and Domain is not dependant on any other layer.

Application -> API
Domain -> Application
Domain, Application -> Infrastructure

## Business Logic
First task is to identify think about the business logic we want to apply and the objects that are related to them.

### Entities
- Product
- Category
- Customer
- Order
- OrderItem

![Aggregates](images\Aggregates%20tables.png)

### Order Item
The idea behind ordering an Item is that it may contain an Order may contain many OrderItem(s), therefore we would want for these items to be altered in any way only by the Order Class. This can be seen as most operations reflect that relation between both classes. 
The Order class also contains a state machine that reflects a choice in its business logic, to better explain this state machine, its flow can be seen below:

![OrderStatus state machine](images\OrderStatusStateMachine.png)
