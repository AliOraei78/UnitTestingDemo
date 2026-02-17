namespace CoreLogic;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException("Denominator cannot be zero.");

        return numerator / denominator;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public IEnumerable<int> GetEvenNumbersUpTo(int max)
    {
        for (int i = 0; i <= max; i += 2)
        {
            yield return i;
        }
    }
}