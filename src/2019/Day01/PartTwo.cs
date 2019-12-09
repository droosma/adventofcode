using FluentAssertions;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day01
{
    public class PartTwo
    {
        [Theory]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void FromExample(decimal mass, decimal fuelRequirement)
        {
            var result = FuelCalculator.ByMassWithFuel(mass);
            result.Should().Be(fuelRequirement);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input-part-one.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly);
            var fuelRequirements = input.Split(Environment.NewLine)
                .AsParallel()
                .Select(value => decimal.Parse(value))
                .Select(mass => FuelCalculator.ByMassWithFuel(mass))
                .Sum();

            fuelRequirements.Should().Be(4876469);
        }
    }
}