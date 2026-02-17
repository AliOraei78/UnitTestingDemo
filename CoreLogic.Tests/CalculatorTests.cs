using CoreLogic;
using Xunit;

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

        // Act
        int result = calc.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var calc = new Calculator();

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
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
}