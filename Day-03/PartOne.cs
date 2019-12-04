using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day_03
{
    public class PartOne
    {
        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void FromExample(string inputOne, string inputTwo, int expectedResult)
        {
            var center = Point.Create(0, 0, ' ');

            var graphOne = Graph.From(center);
            foreach (var vector in inputOne.Split(',')
                                           .Select(Vector.Create))
            {
                graphOne.Draw(vector);
            }

            var graphTwo = Graph.From(center);
            foreach (var vector in inputTwo.Split(',')
                                           .Select(Vector.Create))
            {
                graphTwo.Draw(vector);
            }

            File.WriteAllText("output-one", graphOne.ToString());
            File.WriteAllText("output-two", graphTwo.ToString());

            var intersections = graphOne.Intersections(graphTwo);
            var manhattanDistance = intersections.Select(i => i.DistanceTo(center)).Min();
            manhattanDistance.Should().Be(expectedResult);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly)
                                                  .Split(Environment.NewLine);

            var inputOne = input[0];
            var inputTwo = input[1];

            var center = Point.Create(0, 0, ' ');

            var graphOne = Graph.From(center);
            foreach (var vector in inputOne.Split(',')
                                           .Select(Vector.Create))
            {
                graphOne.Draw(vector);
            }

            var graphTwo = Graph.From(center);
            foreach (var vector in inputTwo.Split(',')
                                           .Select(Vector.Create))
            {
                graphTwo.Draw(vector);
            }

            var intersections = graphOne.Intersections(graphTwo);
            var manhattanDistance = intersections.Select(i => i.DistanceTo(center)).Min();
            manhattanDistance.Should().Be(5357);
        }
    }
}
