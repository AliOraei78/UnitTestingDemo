using System.Collections;
using System.Collections.Generic;

namespace CoreLogic.Tests;

public class CalculatorDivisionData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 10, 2, 5 };
        yield return new object[] { 20, 5, 4 };
        yield return new object[] { -15, 3, -5 };
        yield return new object[] { 100, 10, 10 };
        yield return new object[] { 0, 7, 0 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}