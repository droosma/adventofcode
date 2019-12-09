using FluentAssertions;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Day_05
{
    public class PartOne
    {
        private readonly OptCodeComputer _optCodeComputer;
        private readonly ITestOutputHelper output;

        public PartOne(ITestOutputHelper output)
        {
            var recorder = InMemoryComputationRecorder.ToTestOutputHelper(output);
            _optCodeComputer = new OptCodeComputer(recorder, 1);
            this.output = output;
        }

        [Fact]
        public void Input()
        {
            int[] sequence = new int[] { 3, 2, 0 };
            const int input = 99;
            int[] expectedResult = new int[] { 3, 2, input };

            var recorder = InMemoryComputationRecorder.ToTestOutputHelper(output);
            var optComputer = new OptCodeComputer(recorder, input);
            optComputer.Output(ref sequence);

            sequence.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Multiplication()
        {
            int[] sequence = new int[] { 2, 3, 3, 3, 99 };
            int[] expectedResult = new int[] { 2, 3, 3, 9, 99 };

            _optCodeComputer.Output(ref sequence);

            sequence.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Addition()
        {
            int[] sequence       = new int[] { 1, 2, 2, 3, 99 };
            int[] expectedResult = new int[] { 1, 2, 2, 4, 99 };

            _optCodeComputer.Output(ref sequence);

            sequence.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("1002,4,3,4,33", "1002,4,3,4,99")]
        [InlineData("1101,100,-1,4,0", "1101,100,-1,4,99")]
        public void FromExamples(string input, string expectedOutput)
        {
            var memory = input.Split(",")
                              .Select(int.Parse)
                              .ToArray();

            _optCodeComputer.Output(ref memory);

            string.Join(',', memory).Should().Be(expectedOutput);
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

            //4283952
        }
    }
}
