using FluentAssertions;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day06
{
    public class PartTwo
    {
        [Theory]
        [InlineData("YOU", "SAN", 4)]
        public void FromExample(string source, string destination, int expectedTransfers)
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
                          K)L
                          K)YOU
                          I)SAN";

            var orbits = input.Split(Environment.NewLine)
                              .Select(Orbit.From);

            var center = Entity.From("COM");
            System system = System.Construct(center, orbits);

            var sourceSystem = system.Find(source);
            var destinationSystem = system.Find(destination);

            var sourcePath = System.Path(sourceSystem)
                                   .Skip(1) //remove self.. bah
                                   .ToList();
            var destinationPath = System.Path(destinationSystem)
                                        .Skip(1) //remove self.. bah
                                        .ToList();

            sourcePath.Reverse();
            destinationPath.Reverse();

            var sourcePathIndex = sourcePath.Intersect(destinationPath)
                                            .Max(match => sourcePath.IndexOf(match));
            var destinationPathIndex = destinationPath.Intersect(sourcePath)
                                                      .Max(match => destinationPath.IndexOf(match));
            var commonAncestor = sourcePath[sourcePathIndex];

            var sourceTransfers = sourcePath.Count - sourcePathIndex - 1;
            var destinationTransfers = destinationPath.Count - destinationPathIndex - 1;

            var totalTransfers = sourceTransfers + destinationTransfers;

            totalTransfers.Should().Be(expectedTransfers);
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

            var sourceSystem = system.Find("YOU");
            var destinationSystem = system.Find("SAN");

            var sourcePath = System.Path(sourceSystem)
                                   .Skip(1) //remove self.. bah
                                   .ToList();
            var destinationPath = System.Path(destinationSystem)
                                        .Skip(1) //remove self.. bah
                                        .ToList();

            sourcePath.Reverse();
            destinationPath.Reverse();

            var sourcePathIndex = sourcePath.Intersect(destinationPath)
                                            .Max(match => sourcePath.IndexOf(match));
            var destinationPathIndex = destinationPath.Intersect(sourcePath)
                                                      .Max(match => destinationPath.IndexOf(match));
            var commonAncestor = sourcePath[sourcePathIndex];

            var sourceTransfers = sourcePath.Count - sourcePathIndex - 1;
            var destinationTransfers = destinationPath.Count - destinationPathIndex - 1;

            var totalTransfers = sourceTransfers + destinationTransfers;

            totalTransfers.Should().Be(373);
        }
    }
}
