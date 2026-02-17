# Unit Testing Demo

## Day 1 – Project Setup & Basic Unit Testing Concepts

- Solution created with .NET 8.0 / .NET 9.0
- Projects: 
  - CoreLogic (Class Library)
  - CoreLogic.Tests (xUnit Test Project)
- Installed: xUnit + xunit.runner.visualstudio
- First test written and passed

## Day 2 – xUnit Fundamentals

- Implemented Calculator class with Add, Divide, IsEven methods
- Wrote tests using:
  - [Fact] for single-condition tests
  - [Theory] + [InlineData] for parameterized tests
  - [Theory] + [MemberData] for reusable/complex test data
  - Exception testing with Assert.Throws
- Demonstrated test lifecycle with constructor and IDisposable

## Day 3 – FluentAssertions

- Installed FluentAssertions (latest stable version)
- Replaced standard xUnit assertions with fluent syntax
- Key assertions covered:
  - Should().Be()
  - Should().BeEquivalentTo() vs Should().Equal()
  - Should().Throw<T>() / ThrowExactly<T>() + WithMessage
  - Collection assertions: HaveCount, Contain, OnlyContain, ContainInOrder
- Demonstrated chaining: .And for multiple assertions
- Improved test readability and failure messages

## Day 4 – Mocking with Moq

- Installed Moq (latest stable version)
- Created IUserRepository and UserService with dependency injection
- Key Moq features demonstrated:
  - Setup + Returns for value returning methods
  - Setup + Throws for exception simulation
  - Verify(Times.Once/Never/Exactly) for interaction verification
  - Callback for side-effect tracking
  - It.Is / It.IsAny for parameter matching
- Combined Moq with FluentAssertions for expressive tests

## Day 6 – Professional Logging with Serilog

- Installed: Serilog, Serilog.Sinks.Console, Serilog.Sinks.File, Serilog.Sinks.InMemory (+ Assertions)
- Configured global Logger with minimum Debug level, console & daily rolling file sinks
- Integrated structured logging (ForContext, {Property}) in UserService
- Used appropriate log levels: Debug, Information, Warning, Error
- Tested logging behavior in unit tests using InMemorySink + FluentAssertions

## Day 7 – Logging Visualization with Seq

- Installed Seq locally (via MSI installer or Docker)
- Added Serilog.Sinks.Seq NuGet package
- Extended LoggerConfig to include Seq sink[](http://localhost:5341)
- Verified end-to-end logging: logs appear in Seq dashboard in real-time
- Demonstrated basic queries/filters: Level=, property filters, text search, signals
- Explored structured properties and event details in Seq UI

