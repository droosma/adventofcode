using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Day07
{
    public class PartOne
    {
        private readonly ITestOutputHelper _outputHelper;

        public PartOne(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "4,3,2,1,0", 43210)]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0", "0,1,2,3,4", 54321)]
        [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0", "1,0,4,3,2", 65210)]
        public void FromExample(string programInput, string optimalInput, int expectedOutput)
        {
            var input = optimalInput.Split(',')
                                    .Select(int.Parse)
                                    .ToArray();
            var program = programInput.Split(',')
                                      .Select(int.Parse)
                                      .ToArray();

            var output = Compute(input, program);

            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void FromInput()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input.txt";
            var rawInput = Utilities.EmbeddedResource.Get(resourceName, assembly);
            
            var program = rawInput.Split(',')
                                  .Select(int.Parse)
                                  .ToArray();

            var inputs = GenerateInput().Select(combination => new
                                {
                                    combination = string.Join(',', combination),
                                    signal = Compute(combination, program)
                                }).ToList();

            var x = inputs.Where(i => i.signal > 0).ToList();

            var maxSignal = x.Max(b => b.signal);
            _outputHelper.WriteLine(maxSignal.ToString());
            _outputHelper.WriteLine(string.Join(Environment.NewLine, x.Select(b => $"{b.combination} - {b.signal}")));

            static IEnumerable<int[]> GenerateInput() 
                => Enumerable.Range(0, 100000)
                             .Select(option => option.ToString("00000.##"))
                             .Select(optionString => optionString.Select(c => int.Parse(c.ToString()))
                             .ToArray());
        }

        private int Compute(int[] combination, int[] program)
        {
            try
            {
                var optCodeComputerAmpARecorder = new List<InstructionLog>().ToComputationRecorder();
                var ampAComputer = new OptCodeComputer(optCodeComputerAmpARecorder, program);

                ampAComputer.WithInput(combination[0]);
                ampAComputer.WithInput(0);
                ampAComputer.Calculate();

                var optCodeComputerAmpBRecorder = new List<InstructionLog>().ToComputationRecorder();
                var ampBComputer = new OptCodeComputer(optCodeComputerAmpBRecorder, program);

                ampBComputer.WithInput(combination[1]);
                ampBComputer.WithInput(ampAComputer.Output);
                ampBComputer.Calculate();

                var optCodeComputerAmpCRecorder = new List<InstructionLog>().ToComputationRecorder();
                var ampCComputer = new OptCodeComputer(optCodeComputerAmpCRecorder, program);

                ampCComputer.WithInput(combination[2]);
                ampCComputer.WithInput(ampBComputer.Output);
                ampCComputer.Calculate();

                var optCodeComputerAmpDRecorder = new List<InstructionLog>().ToComputationRecorder();
                var ampDComputer = new OptCodeComputer(optCodeComputerAmpDRecorder, program);

                ampDComputer.WithInput(combination[3]);
                ampDComputer.WithInput(ampCComputer.Output);
                ampDComputer.Calculate();

                var optCodeComputerAmpERecorder = new List<InstructionLog>().ToComputationRecorder();
                var ampEComputer = new OptCodeComputer(optCodeComputerAmpERecorder, program);

                ampEComputer.WithInput(combination[4]);
                ampEComputer.WithInput(ampDComputer.Output);
                ampEComputer.Calculate();

                return ampEComputer.Output;
            }
            catch (InputNotProvided ex) 
            {
                _outputHelper.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
