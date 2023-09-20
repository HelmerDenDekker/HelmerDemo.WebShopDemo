# Finding the right architecture 

I want to learn about domain-driven design and have a show-case project.

The desired qualities are:
- Maintainability: Testable, understable and modifiable
- Modifiability: The cost of introducing change should be as low as possible
- Testability: Being able to do systematic testing

Challenges and desired Outcome:
- Finding the right software architecture
- Defining a domain model

Forces:
- Use dotNET technology

## Status
Under revision

## Decision 

Implement a stream-based architecture as presented in the software architecture course by vijfhart.


## Rationale 

The idea is to use a webshop-domain to learn - and have a showcase for a stream based architecture.
dotNET is used as a techstack, because I as a programmer have the most experience with dotNET and C# applications.
The cost of initial development time is not an issue.

It is loosely based on [eShopOnContainers by microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/cloud-native/introduce-eshoponcontainers-reference-app)

### CatalogService

Simple Data driven and CRUD microservice

### OrderService

Domain driven design showcase

### API Gateway

For now: YARP as BFF, because I know this technology. But I am not happy:
It acts as BFF which:
- is a security pro, sessions are managed by the server and user data is secure
- is a scalability issue, because I have tight coupling with users session and I need a session for each user on my server

Both of these qualities are not mentioned in my "desired qualities". so for now, I will go ahead with this techstack.



## Consequences

For DDD:

### Advantages

- The software is a reflection of the real world domain:
- A domain model improves understanding
- A domain model validates understanding
- A domain model improves communication
- Easier to write tests
- Easier to maintain code (interfaces stay the same, layers do not corrupt each other)


### Disadvantages

- Knowledge crunching, you need a domain expert
- A long development time (time-to-market), due to the need to design and implement the domain model, data transfer objects, data access objects, and their adapters. Most of this happens before the UI is created. We should mitigate this risk.
- Interfaces abstract the implementation away, making it hard to debug.