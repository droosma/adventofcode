using System;
using System.Linq;

namespace Day05
{
    public class OptCodeComputer
    {
        private readonly ComputationRecorder _recorder;
        private readonly int _input;

        public OptCodeComputer(ComputationRecorder recorder, int input)
        {
            _recorder = recorder;
            _input = input;
        }

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

                switch (optCode.Type)
                {
                    case InstructionType.exit:
                        return -1;
                    case InstructionType.Addition:
                        return index + Addition(optCode, ref source, index);
                    case InstructionType.Multiplication:
                        return index + Multiplication(optCode, ref source, index);
                    case InstructionType.input:
                        return index + Input(ref source, index);
                    case InstructionType.output:
                        return index + Output(optCode, ref source, index);
                    case InstructionType.jumpIfTrue:
                        return JumpIfTrue(optCode, ref source, index);
                    case InstructionType.jumpIfFalse:
                        return JumpIfFalse(optCode, ref source, index);
                    case InstructionType.lessThan:
                        return index + LessThan(optCode, ref source, index);
                    case InstructionType.equals:
                        return index + Equals(optCode, ref source, index);
                }

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
            int Output(OptCode optCode, ref int[] source, int index)
            {
                var valueIndex = index + 1;
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? source[source[valueIndex]]
                    : source[valueIndex];

                _recorder.Output(index, valueIndex, firstParameter);

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
