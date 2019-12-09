using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day06
{
    public class PartOne
    {
        [Theory]
        [InlineData("D", 3)]
        [InlineData("L", 7)]
        [InlineData("COM", 0)]
        public void FromExample(string entity, int expectedDepth)
        {
            var input = @"COM)B
                          B)C
                          C)D
                          D)E
                          E)F
                          B)G
                          G)H
                          D)I
                          E)J
                          J)K
                          K)L";

            var orbits = input.Split(Environment.NewLine)
                              .Select(Orbit.From);

            var center = Entity.From("COM");
            System system = System.Construct(center, orbits);

            var depth = system.Trace(entity);

            depth.Should().Be(expectedDepth);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly);

            var orbits = input.Split(Environment.NewLine)
                              .Select(Orbit.From);

            var center = Entity.From("COM");
            System system = System.Construct(center, orbits);

            var totalDepth = orbits.SelectMany(o => new List<Entity>() { o.Center, o.Satellite })
                                   .Distinct()
                                   .Sum(entity => system.Trace(entity));

            totalDepth.Should().Be(160040);
        }
    }
}
