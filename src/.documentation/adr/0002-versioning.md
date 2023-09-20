# Implement versioning for the API

Challenge: In the API we want to support multiple versions

## Status
Accepted

## Decision 

We will not implement our own solution, but use one out-of-the-box: 
https://blog.christian-schou.dk/how-to-use-api-versioning-in-net-core-web-api/

## Rationale 

I did not experiment with anything else, just a quick versioning fix here.
We will use the APIVersion attribute, since we have a small new product here.

## Consequences

Using the APIVersion attribute may lead to a very large controller and mismatch. If needed we can refactor to a more structured approach later, using v1 v2 folders for the controllers.