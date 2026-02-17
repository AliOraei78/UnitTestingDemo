using AutoFixture;
using FluentAssertions;
using Xunit;

namespace CoreLogic.Tests;

public class AutoFixtureBasicsTests
{
    [Fact]
    public void Create_SimpleTypes_GeneratesReasonableValues()
    {
        var fixture = new Fixture();

        string name = fixture.Create<string>();
        int age = fixture.Create<int>();
        bool isActive = fixture.Create<bool>();
        DateTime created = fixture.Create<DateTime>();

        name.Should().NotBeNullOrEmpty();
        age.Should().BeInRange(-10000, 10000);
        isActive.Should().Be(isActive == true || isActive == false);
        created.Should().BeAfter(DateTime.MinValue);
    }
}