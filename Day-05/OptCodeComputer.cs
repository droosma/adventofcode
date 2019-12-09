using System;
using System.Linq;
using Xunit.Abstractions;

namespace Day_05
{
    public class OptCodeComputer
    {
        private readonly ITestOutputHelper _output;
        private readonly int _input;

        public OptCodeComputer(ITestOutputHelper output, int input)
        {
            _output = output;
            _input = input;
        }

        const int exit = 99;
        const int add = 1;
        const int multiply = 2;
        const int input = 3;
        const int output = 4;
        const int jumpIfTrue = 5;
        const int jumpIfFalse = 6;
        const int lessThan = 7;
        const int equals = 8;

        public void Output(ref int[] source)
        {
            var index = 0;
            var iteration = 0;
            while ((index = Compute(ref source, index)) > 0) ;

            int Compute(ref int[] source, int index)
            {
                _output.WriteLine($"iteration: {++iteration}");

                var instructionCode = source.Skip(index).First();
                var optCode = new OptCode(instructionCode);

                if (optCode.Instruction == exit)
                    return -1;
                else if (optCode.Instruction == add)
                    return index + Addition(optCode, ref source, index);
                else if (optCode.Instruction == multiply)
                    return index + Multiplication(optCode, ref source, index);
                else if (optCode.Instruction == input)
                    return index + Input(ref source, index);
                else if (optCode.Instruction == output)
                    return index + Output(ref source, index);
                else if (optCode.Instruction == jumpIfTrue)
                    return JumpIfTrue(optCode, ref source, index);
                else if (optCode.Instruction == jumpIfFalse)
                    return JumpIfFalse(optCode, ref source, index);
                else if (optCode.Instruction == lessThan)
                    return index + LessThan(optCode, ref source, index);
                else if (optCode.Instruction == equals)
                    return index + Equals(optCode, ref source, index);

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

                int userInput = _input;
                source[destination] = userInput;
                return 2;
            }
            int Output(ref int[] source, int index)
            {
                var value = source[index + 1];
                _output.WriteLine($"Requested value @ {index + 1}: {value}");

                return 2;
            }
            int JumpIfTrue(OptCode optCode, ref int[] source, int index)
            {
                var value = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var destination = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];

                int result;
                if (value != 0)
                    result = destination;
                else
                    result = index + 3;

                _output.WriteLine($"Jump if true value {value} with result : {result}");
                return result;
            }
            int JumpIfFalse(OptCode optCode, ref int[] source, int index)
            {
                var value = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var destination = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];

                int result;
                if (value == 0)
                    result = destination;
                else
                    result = index + 3;

                _output.WriteLine($"Jump if false value {value} with result : {result}");
                return result;
            }
            int LessThan(OptCode optCode, ref int[] source, int index)
            {
                var first = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var second = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                source[destination] = first < second ? 1 : 0;

                _output.WriteLine($"LessThan first: {first} second : {second} destination: {destination} result: {source[destination]}");
                return 4;
            }
            int Equals(OptCode optCode, ref int[] source, int index)
            {
                var first = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var second = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                source[destination] = first == second ? 1 : 0;

                _output.WriteLine($"LessThan first: {first} second : {second} destination: {destination} result: {source[destination]}");
                return 4;
            }
        }
    }
}
