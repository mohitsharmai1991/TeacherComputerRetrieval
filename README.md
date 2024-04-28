Design and Assumptions :

Design : 

- Basic usage of interfaces following SOLID principles specifically Single responsibility and Open Closed principles.
- There is an IExecute to run the app which is being called from Program.cs and IExecute interacts with the IRoute to perform operations.
- Ideally if the scope is higher we can divide the repositories into different layers of projects.
- Proper injection is not used as it is a small level application but you can get a hint of how it can be used inside the constructors.
- Exception handling is done to avoid negative and failure scenarios.
- Basic Unit test cases are covered, I can write tons of unit test cases, but to give a hint on how to write I have covered the basics and few edge cases.
- Code is readable in itself, followed coding standards.
- Code is production ready and optimized to an extent but it can be more clean and optimized to make it more efficient and bug proof, provided I spend more time on it.
- NUnit tests are writen in TeacherComputerRetrieval.Tests project inside RouteTests.cs file.

Assumptions :

- Distance between 2 academies can not be 0.
- Distance should be positive and can be more than 1 digit number.
- Input is case sensitive.
- Routes can be traversed multiple time in some scenarios as mentioned in the question.
- Start and end academies can not be same in input.
- No duplicate routes allowed in input with respect to start and end academies.

How to run the application?

- Coded in Visual studio 2022 with .Net 8.0 and it is a console application. NUnit is used to write unit tests.
- Open the Solution TeacherComputerRetrieval.sln in Visual studio 2022.
- Make TeacherComputerRetrieval project as startup project.
- Build the Solution.
- Run the TeacherComputerRetrieval project.
