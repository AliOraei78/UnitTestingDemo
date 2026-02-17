using CoreLogic;
using Xunit;
using FluentAssertions;

namespace CoreLogic.Tests;

public class CalculatorTests : IDisposable
{
    private readonly Calculator _calculator;
    private bool _disposed;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
    }

    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
    {
        // Arrange
        var calc = new Calculator();
        int a = 7;
        int b = 4;

        // Act
        int result = calc.Add(a, b);

        // Assert
        Assert.Equal(11, result);
    }

    [Theory]
    [InlineData(5, 3, 8)]
    [InlineData(0, 0, 0)]
    [InlineData(-2, 7, 5)]
    [InlineData(100, -50, 50)]
    public void Add_VariousInputs_ReturnsCorrectSum(int a, int b, int expected)
    {
        // Arrange
        var calc = new Calculator();

        calc.Add(a, b).Should().Be(expected);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var calc = new Calculator();

        // Act & Assert
        Action act = () => calc.Divide(10, 0);
        act.Should().Throw<DivideByZeroException>().WithMessage("Denominator cannot be zero.");
    }

    [Theory]
    [InlineData(4, true)]
    [InlineData(7, false)]
    [InlineData(0, true)]
    [InlineData(-6, true)]
    public void IsEven_Number_ReturnsCorrectParity(int number, bool expectedParity)
    {
        // Arrange
        var calc = new Calculator();

        // Act
        bool result = calc.IsEven(number);

        // Assert
        Assert.Equal(expectedParity, result);
    }

    public static IEnumerable<object[]> DivisionTestCases()
    {
        yield return new object[] { 10, 2, 5 };
        yield return new object[] { 20, 5, 4 };
        yield return new object[] { -15, 3, -5 };
    }

    [Theory]
    [MemberData(nameof(DivisionTestCases))]
    public void Divide_ValidInputs_ReturnsCorrectQuotient(int numerator, int denominator, int expected)
    {
        // Arrange
        var calc = new Calculator();

        // Act
        int result = calc.Divide(numerator, denominator);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(CalculatorDivisionData))]
    public void Divide_ValidInputs_ReturnsCorrectQuotient_ClassData(
    int numerator,
    int denominator,
    int expected)
    {
        // Arrange
        var calc = new Calculator();

        // Act
        int result = calc.Divide(numerator, denominator);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetEvenNumbersUpTo_ReturnsCorrectSequence()
    {
        var calc = new Calculator();

        var result = calc.GetEvenNumbersUpTo(10).ToList();

        result.Should().BeEquivalentTo(new[] { 0, 2, 4, 6, 8, 10 });
    }

    [Fact]
    public void GetEvenNumbersUpTo_ReturnsCorrectSequenceInOrder()
    {
        var calc = new Calculator();

        var result = calc.GetEvenNumbersUpTo(10).ToList();

        result.Should().Equal(0, 2, 4, 6, 8, 10 );
    }

    [Fact]
    public void Calculator_BasicOperations_ChainExample()
    {
        var calc = new Calculator();

        calc.Add(5, 3)
            .Should()
            .Be(8)
            .And
            .BePositive()
            .And
            .NotBe(0);

        calc.Divide(20, 4)
            .Should()
            .Be(5)
            .And
            .BeGreaterThan(0);
    }

    [Fact]
    public void List_Chain()
    {
        var numbers = new List<int> { 2, 4, 6, 8 };

        numbers.Should()
               .NotBeEmpty()
               .And
               .HaveCount(4)
               .And
               .OnlyContain(n => n % 2 == 0)
               .And
               .ContainInOrder(2, 4);
    }
}