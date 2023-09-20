# Validation in domain-driven design 

From the course: In domain-driven design the classes should be self-validating
Minimum information principle.


## Status
Proposed

## Decision 

I will play around with both concepts in this repository to see the benefits or pitfalls.

For user-oriented failures:
- Use a Result class

For developer-oriented failures:
- Use the custom exceptions

For example 
- the user wants to create an account that already exists : Result
- the developer wants to access a property that should not be accessed -> Custom Error.

Documentation will be part of the code.

## Rationale 

### Option 1: Custom exceptions

Use custom exceptions whenever something goes wrong (CustomerAlreadyExistsException for a Customer that already exists).

Benefits:
- Defensive. Whenever the code hits an exception we will not proceed and the domain object will not be created.
- Stack trace (in the upper layers.)
- Easier debugging. 
- Clear clean-cut exceptions which will be clear to the Domain experts

Disadvantages
- Performance. The code will break, without the use knowing why. The program will not proceed and not give feedback.
- Does not obey minimum information principle
- It will be a lot of classes to generate

### Option 2: Use default asp.net validation

If we use it in the DTO's, a default specified, but generic exception will be thrown to the user from the controller. The validation is no longer in the domain model.

The default validation is fine for the user, but not for debugging as a developer.
Using multiple kinds of validation and their place is not clear for the developer, stick to one standard.
This will only work on the DTO's, which is not DDD, so we wont use this


### Option 3: Custom validation with Result class

Benefits:
- Expressive, reading the code you know the Result of a method, so you know what happened (instead of it breaking due to thrown error)
- Performance (for the user) the application does not have a failure, it just continues and gives feedback
- Usability
- Self-documenting, if you add all of the errors to a single file with constants

Disadvantages
 - There is no stack trace. So debugging is more difficult, you need a logging system to log the stack trace.

## Consequences
Describe here the resulting context, after applying the decision. All consequences should be listed, not just the "positive" ones. 