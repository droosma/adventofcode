using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Day05
{
    public class PartTwo
    {
        private readonly IList<InstructionLog> _instructions = new List<InstructionLog>();
        private readonly OptCodeComputer _optCodeComputer;

        public PartTwo()
        {
            var recorder = _instructions.ToComputationRecorder();
            _optCodeComputer = new OptCodeComputer(recorder, 5);
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

            var output = _instructions.First(i => i.Type == InstructionType.output);

            output.Output.Should().Contain("4283952");
        }
    }
}
