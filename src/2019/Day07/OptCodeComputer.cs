using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    public class OptCodeComputer
    {
        private readonly ComputationRecorder _recorder;
        private readonly List<int> _inputs = new List<int>();
        private int _inputCounter = 0;

        public int Output { get; private set; }
        public int[] Memory { get; }

        public OptCodeComputer(int[] memory)
        {
            Memory = memory;
        }

        public OptCodeComputer(ComputationRecorder recorder, 
                               int[] memory) : this(memory)
        {
            _recorder = recorder;
        }

        public OptCodeComputer WithInput(int input)
        {
            _inputs.Add(input);
            return this;
        }

        public void Execute()
        {
            var index = 0;
            var iteration = 0;
            while ((index = Compute(index)) > 0) ;

            int Compute(int index)
            {
                iteration++;

                var instructionCode = Memory.Skip(index).First();
                var optCode = new OptCode(instructionCode);

                switch (optCode.Type)
                {
                    case InstructionType.exit:
                        return -1;
                    case InstructionType.Addition:
                        return index + Addition(optCode, index);
                    case InstructionType.Multiplication:
                        return index + Multiplication(optCode, index);
                    case InstructionType.input:
                        return index + Input(index);
                    case InstructionType.output:
                        return index + Output(optCode, index);
                    case InstructionType.jumpIfTrue:
                        return JumpIfTrue(optCode, index);
                    case InstructionType.jumpIfFalse:
                        return JumpIfFalse(optCode, index);
                    case InstructionType.lessThan:
                        return index + LessThan(optCode, index);
                    case InstructionType.equals:
                        return index + Equals(optCode, index);
                }

                _recorder.Failure(optCode);
                throw ProgramFailure.Create(optCode, index);
            }

            int Addition(OptCode optCode, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[index + 1]]
                    : Memory[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? Memory[Memory[index + 2]]
                    : Memory[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? Memory[index + 3]
                    : Memory[Memory[index + 3]];

                var result = firstParameter + secondParameter;
                Memory[destination] = result;

                _recorder?.Addition(index, optCode, firstParameter, secondParameter, result, destination);

                return 4;
            }
            int Multiplication(OptCode optCode, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[index + 1]]
                    : Memory[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? Memory[Memory[index + 2]]
                    : Memory[index + 2];
                var destination = optCode.ThirdParameterIsPositionMode
                    ? Memory[index + 3]
                    : Memory[Memory[index + 3]];

                var result = firstParameter * secondParameter;
                Memory[destination] = result;

                _recorder?.Multiplication(index, optCode, firstParameter, secondParameter, result, destination);

                return 4;
            }
            int Input(int index)
            {
                var firstParameter = Memory[index + 1];

                if(_inputCounter >= _inputs.Count())
                    throw InputNotProvided.Create(index);

                int userInput = _inputs[_inputCounter];
                _inputCounter++;
                Memory[firstParameter] = userInput;

                _recorder?.Input(index, firstParameter, userInput);

                return 2;
            }
            int Output(OptCode optCode, int index)
            {
                var valueIndex = index + 1;
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[valueIndex]]
                    : Memory[valueIndex];

                _recorder?.Output(index, valueIndex, firstParameter);
                this.Output = firstParameter;
                return 2;
            }
            int JumpIfTrue(OptCode optCode, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[index + 1]]
                    : Memory[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? Memory[Memory[index + 2]]
                    : Memory[index + 2];

                int result;
                if (firstParameter != 0)
                    result = secondParameter;
                else
                    result = index + 3;

                _recorder?.JumpIfTrue(index, optCode, firstParameter, secondParameter, result);

                return result;
            }
            int JumpIfFalse(OptCode optCode, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[index + 1]]
                    : Memory[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? Memory[Memory[index + 2]]
                    : Memory[index + 2];

                int result;
                if (firstParameter == 0)
                    result = secondParameter;
                else
                    result = index + 3;

                _recorder?.JumpIfFalse(index, optCode, firstParameter, secondParameter, result);
                return result;
            }
            int LessThan(OptCode optCode, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[index + 1]]
                    : Memory[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? Memory[Memory[index + 2]]
                    : Memory[index + 2];
                var thirdParameter = optCode.ThirdParameterIsPositionMode
                    ? Memory[index + 3]
                    : Memory[Memory[index + 3]];

                var result = firstParameter < secondParameter ? 1 : 0;
                Memory[thirdParameter] = result;

                _recorder?.LessThan(index, optCode, firstParameter, secondParameter, thirdParameter, result);

                return 4;
            }
            int Equals(OptCode optCode, int index)
            {
                var firstParameter = optCode.FirstParameterIsPositionMode
                    ? Memory[Memory[index + 1]]
                    : Memory[index + 1];
                var secondParameter = optCode.SecondParameterIsPositionMode
                    ? Memory[Memory[index + 2]]
                    : Memory[index + 2];
                var thirdParameter = optCode.ThirdParameterIsPositionMode
                    ? Memory[index + 3]
                    : Memory[Memory[index + 3]];

                var result = firstParameter == secondParameter ? 1 : 0;
                Memory[thirdParameter] = result;

                _recorder?.Equals(index, optCode, firstParameter, secondParameter, thirdParameter, result);

                return 4;
            }
        }
    }
}
