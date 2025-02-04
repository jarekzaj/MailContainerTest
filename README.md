### Mail Container Test 

The code for this exercise has been developed to manage the transfer of mail items from one container to another for processing.

#### Process for transferring mail

- Lookup the container the mail is being transferred from.
- Check the containers are in a valid state for the transfer to take place.
- Reduce the container capacity on the source container and increase the destination container capacity by the same amount.

#### Restrictions

- A container can only hold one type of mail.


#### Assumptions

- For the sake of simplicity, we can assume the containers have an unlimited capacity.

### The exercise brief

The exercise is to take the code in the solution and refactor it into a more suitable approach with the following things in mind:

- Testability
- Readability
- SOLID principles
- Architectural design of the code

You should not change the method signature of the MakeMailTransfer method.

You should add suitable tests into the MailContainerTest.Test project.

There are no additional constraints, use the packages and approach you feel appropriate, aim to spend no more than 2 hours. Please update the readme with specific comments on any areas that are unfinished and what you would cover given more time.


### TODO

Enums StandardLetter, LargeLetter and SmallParcel could be changed to classes sharing a common interface or a base class with a Validate method. 

Possibly use a library for validation.

Ideally more unit tests which cover things like nullable reference types (strings) in the solution.

More error handling.

Currently very basic separation of concern. 

Some more code de-duplication could be achieved. 

Identified an issue with AllowedMailTypes, which currently does a bit comparison - I believe this is not intended behaviour and has been identified in a couple of unit tests.


