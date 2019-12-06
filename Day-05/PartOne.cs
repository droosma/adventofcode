using FluentAssertions;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Day_05
{
    public class PartOne
    {
        private readonly OptCodeComputer _optCodeComputer;

        public PartOne(ITestOutputHelper output)
        {
            _optCodeComputer = new OptCodeComputer(output);
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
        }
    }

    public class OptCode
    {
        public int Instruction { get; }
        public bool FirstParameterIsPositionMode { get; }
        public bool SecondParameterIsPositionMode { get; }
        public bool ThirdParameterIsPositionMode { get; }

        public OptCode(int code)
        {
            string format = "00000.##";
            var input = code.ToString(format);

            Instruction = int.Parse(input.Substring(3, 2));
            FirstParameterIsPositionMode = input[2].ToString() == "0" ? true : false;
            SecondParameterIsPositionMode = input[1].ToString() == "0" ? true : false;
            ThirdParameterIsPositionMode = input[0].ToString() == "0" ? true : false;
        }
    }

    public class OptCodeComputer
    {
        private readonly ITestOutputHelper _output;

        public OptCodeComputer(ITestOutputHelper output)
        {
            _output = output;
        }

        const int exit = 99;
        const int add = 1;
        const int multiply = 2;
        const int input = 3;
        const int output = 4;

        public void Output(ref int[] source)
        {
            var index = 0;
            var iteration = 0;
            while ((index = Compute(ref source, index, iteration)) > 0) ;

            int Compute(ref int[] source, int index, int iteration)
            {
                _output.WriteLine($"iteration: {iteration++}");

                var instructionCode = source.Skip(index).First();
                var optCode = new OptCode(instructionCode);

                if (optCode.Instruction == exit)
                    return -1;
                else if (optCode.Instruction == add)
                    return index + Addition(optCode, ref source, index);
                else if (optCode.Instruction == multiply)
                    return index + Multiplication(optCode, ref source, index);
                else if(optCode.Instruction == input)
                    return index + Input(ref source, index);
                else if(optCode.Instruction == output)
                    return index + Output(ref source, index);

                throw new Exception("program flaw detected");
            }

            int Multiplication(OptCode optCode, ref int[] source, int index)
            {
                var noun = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var verb = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                var result = noun * verb;
                source[destination] = result;

                _output.WriteLine($"Index: {index} - Multiplication - {noun} * {verb} = {result} @ {destination}");

                return 4;
            }

            int Addition(OptCode optCode, ref int[] source, int index)
            {
                var noun = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var verb = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                var result = noun + verb;
                source[destination] = result;

                _output.WriteLine($"Index: {index} - Addition - {noun} + {verb} = {result} @ {destination}");

                return 4;
            }

            int Input(ref int[] source, int index)
            {
                var destination = source[index + 1];

                int userInput = 1;
                //string rawUserInput;
                //do
                //{
                //    _output.WriteLine("Please provide int input");
                //    rawUserInput = Console.ReadLine();
                //}
                //while (int.TryParse(rawUserInput, out userInput));

                source[destination] = userInput;
                return 2;
            }

            int Output(ref int[] source, int index)
            {
                var value = source[index + 1];
                _output.WriteLine($"Requested value @ {index + 1}: {value}");

                return 2;
            }
        }
    }
}
