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
Accepted

## Decision 

Implement the webshop in dotNET with domain driven design as thought in the software architecture course by vijfhart.
Have three layers as described by Microsoft:
- Application, containing Presentation (API) and Service layers
- Domain, containing the domain model
- Infrastructure, containing Data access and integration

![Layers](/images/0001-layers.png "Layers")

The three layers will be loosely coupled using interfaces.

## Rationale 

The idea is to use a webshop-domain to learn - and have a showcase for domain driven design.
dotNET is used as a techstack, because I as a programmer have the most experience with dotNET and C# applications.
The cost of initial development time is not an issue.


## Consequences

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