using FluentAssertions;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day_01
{
    public class PartOne
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void FromExample(int mass, int expectedFuelRequirements)
        {
            var fuelRequirements = FuelCalculator.ByMass(mass);

            fuelRequirements.Should().Be(expectedFuelRequirements);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input-part-one.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly);
            var fuelRequirements = input.Split(Environment.NewLine)
                .Select(value => decimal.Parse(value))
                .Select(mass => FuelCalculator.ByMass(mass))
                .Sum();

            fuelRequirements.Should().Be(3252897);
        }
    }
}