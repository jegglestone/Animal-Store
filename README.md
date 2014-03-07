Animal-Store
============

Online store for selling animals as pets

This is an application that I've created as a place for browsing and selling animals (well, just dogs for now) as pets whilst allowing me to try out further features of ASP.Net MVC and Web-API and the Entity Framework amongst other things.


##Technology stack

###ASP.Net MVC

###KnockoutJS

###AngularJS

###JQuery

###Web-API

RESTful open standard API serving Http requests in JSON or XML format.

###Entity Framework 5 Code First and Migrations

Repository and Unit of Work patterns used to build an effective and scalable data layer. Used Entity Framework as the ORM and used the code migration model to update and reseed the database when the model changes so the changes can be made and built fast and efficiently.

###Unity IoC Container


###SQL Server

Used Entity framework code-first approach to create the Animals and Places databases.

###Log4Net

Implemented logging into a common project as a cross-cutting concern and used AOP to integrate logging in appropriate parts of the codebase without breaking the Single Responsibility principle and allowing other methods to focus on their primary concerns.

Used lossy logging technique to improve performance and ensure that only a configurable number of errors from the stack are appended to the event log in the event of an exception.

I took the approach of configuring Log4net in code rather than the usual XML approach as I wanted to be able to more quickly spot any problems, errors or omissions and have the benefit of type safety.

###MongoDB

Aim to use this as a caching layer to reduce calls to the API in future.

###NUnit and Rhino Mocks

Used to carry out unit testing of the solution. Developing mainly used TDD (test first) developemenbt to avoid building up technical debt.
Further tests do need adding to get the test coverage increased to 90%, however ;).

###Specflow

Used to cary out BDD and run acceptance tests on the Web application and the API.