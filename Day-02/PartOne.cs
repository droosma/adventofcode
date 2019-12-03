using FluentAssertions;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day_02
{
    public class PartOne
    {
        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void FromExample(string integers, string expectedResult)
        {
            var optCodes = integers.Split(",")
                                   .Select(int.Parse)
                                   .ToArray();

            OptCodeComputer.Output(ref optCodes);

            string.Join(',', optCodes).Should().Be(expectedResult);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly);

            var optCodes = input.Split(",")
                                .Select(int.Parse)
                                .ToArray();

            OptCodeComputer.Output(ref optCodes);

            optCodes.First().Should().Be(5534943);
        }
    }
}
