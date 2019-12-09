using System;
using System.Linq;

namespace Day05
{
    public class OptCodeComputer
    {
        private readonly InMemoryComputationRecorder _recorder;
        private readonly int _input;

        public OptCodeComputer(InMemoryComputationRecorder recorder, int input)
        {
            _recorder = recorder;
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
                iteration++;

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

                _recorder.Failure(optCode);
                throw new Exception("program flaw detected");
            }

            int Multiplication(OptCode optCode, ref int[] source, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                var result = firstParameter * secondParameter;
                source[destination] = result;

                _recorder.Multiplication(index, optCode, firstParameter, secondParameter, result, destination);

                return 4;
            }
            int Addition(OptCode optCode, ref int[] source, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                var result = firstParameter + secondParameter;
                source[destination] = result;

                _recorder.Addition(index, optCode, firstParameter, secondParameter, result, destination);

                return 4;
            }
            int Input(ref int[] source, int index)
            {
                var firstParameter = source[index + 1];

                int userInput = _input;
                source[firstParameter] = userInput;

                _recorder.Input(index, firstParameter, userInput);

                return 2;
            }
            int Output(ref int[] source, int index)
            {
                var valueIndex = index + 1;
                var value = source[valueIndex];

                _recorder.Output(index, valueIndex, value);

                return 2;
            }
            int JumpIfTrue(OptCode optCode, ref int[] source, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];

                int result;
                if (firstParameter != 0)
                    result = secondParameter;
                else
                    result = index + 3;

                _recorder.JumpIfTrue(index, optCode, firstParameter, secondParameter, result);

                return result;
            }
            int JumpIfFalse(OptCode optCode, ref int[] source, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];

                int result;
                if (firstParameter == 0)
                    result = secondParameter;
                else
                    result = index + 3;

                _recorder.JumpIfFalse(index, optCode, firstParameter, secondParameter, result);
                return result;
            }
            int LessThan(OptCode optCode, ref int[] source, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var thirdParameter = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                var result = firstParameter < secondParameter ? 1 : 0;
                source[thirdParameter] = result;

                _recorder.LessThan(index, optCode, firstParameter, secondParameter, thirdParameter, result);

                return 4;
            }
            int Equals(OptCode optCode, ref int[] source, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[index + 1]]
                    : source[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? source[source[index + 2]]
                    : source[index + 2];
                var thirdParameter = optCode.ThirdParameterIsPositionMode
                    ? source[index + 3]
                    : source[source[index + 3]];

                var result = firstParameter == secondParameter ? 1 : 0;
                source[thirdParameter] = result;

                _recorder.Equals(index, optCode, firstParameter, secondParameter, thirdParameter, result);

                return 4;
            }
        }
    }
}
