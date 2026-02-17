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

**Coverage so far**: Basic methods fully tested
