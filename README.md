Animal-Store
============

Online store for selling animals as pets

This is an application that I've created as a place for browsing and selling animals (well, just dogs for now) as pets.

Advertising : local press, forums, Google? Pet magazines.


##Technology stack

###ASP.Net MVC

###KnockoutJS

###AngularJS

###JQuery

###Web-API

Used REST principles to build an open standard API that can return data in JSON or XML format.

###Entity Framework 5 Code First and Migrations

Repository and Unit of Work patterns used to build an effective and scalable data layer. Used Entity Framework as the ORM and used the code migration model to update and reseed the database when the model changes so the changes can be made and built fast and efficiently.

###Unity IoC Container


###SQL Server 2012


###Log4Net

Implemented logging into a common project as a cross-cutting concern and used AOP to integrate logging in appropriate parts of the codebase without breaking the Single Responsibility principle and allowing other methods to focus on their primary concerns.

Used lossy logging technique to improve performance and ensure that only a configurable number of errors from the stack are appended to the event log in the event of an exception.

I took the approach of configuring Log4net in code rather than the usual XML approach as I wanted to be able to more quickly spot any problems, errors or omissions and have the benefit of type safety.

###MongoDB