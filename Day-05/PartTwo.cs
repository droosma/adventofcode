using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Day_05
{
    public class PartTwo
    {
        private readonly OptCodeComputer _optCodeComputer;

        public PartTwo(ITestOutputHelper output)
        {
            _optCodeComputer = new OptCodeComputer(output, 5);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly);

            var memory = input.Split(",")
                              .Select(int.Parse)
                              .ToArray();

            _optCodeComputer.Output(ref memory);
        }
    }
}
