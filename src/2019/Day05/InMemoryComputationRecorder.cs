using System;
using System.Text;

namespace Day05
{

    public class ComputationRecorder
    {
        private readonly Action<InstructionType, int, string> _output;
        private int _count = 0;

        private void Record(InstructionType type, StringBuilder builder)
        {
            _count++;
            _output.Invoke(type, _count, builder.ToString());
        }

        internal ComputationRecorder(Action<InstructionType, int, string> output)
        {
            _output = output;
        }

        public void Addition(int index, OptCode optCode, int firstParameter, int secondParameter, int result, int destination)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Addition on Index {index} with code {optCode}");

            var modeFirst = optCode.FirstParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"First: {firstParameter} [Mode:{modeFirst}]");

            var modeSecond = optCode.SecondParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Second: {secondParameter} [Mode:{modeSecond}]");

            var modeThird = optCode.ThirdParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Result: {result} @ index {destination} [Mode:{modeThird}]");

            Record(InstructionType.Addition, builder);
        }

        public void Equals(int index, OptCode optCode, int firstParameter, int secondParameter, int thirdParameter, int result)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Equals on Index {index} with code {optCode}");

            var modeFirst = optCode.FirstParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"First: {firstParameter} [Mode:{modeFirst}]");

            var modeSecond = optCode.SecondParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Second: {secondParameter} [Mode:{modeSecond}]");

            var modeThird = optCode.ThirdParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Result: {result} @ index {thirdParameter} [Mode:{modeThird}]");

            Record(InstructionType.equals, builder);
        }

        public void Failure(OptCode optCode)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Failed to execute program {optCode}");

            Record(InstructionType.Fault, builder);
        }

        public void Initialize(int[] source)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Initializing");
            builder.AppendLine(string.Join(',', source));

            Record(InstructionType.Initialize, builder);
        }

        public void Input(int index, int firstParameter, int userInput)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Input on Index {index}");
            builder.AppendLine($"First: {firstParameter}");
            builder.AppendLine($"Set {userInput} @ index {firstParameter}");

            Record(InstructionType.input, builder);
        }

        public void JumpIfFalse(int index, OptCode optCode, int firstParameter, int secondParameter, int result)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"JumpIfFalse on Index {index} with code {optCode}");

            var modeFirst = optCode.FirstParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"First: {firstParameter} [Mode:{modeFirst}]");

            var modeSecond = optCode.SecondParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Second: {secondParameter} [Mode:{modeSecond}]");

            builder.AppendLine($"Result: index {result}");

            Record(InstructionType.jumpIfFalse, builder);
        }

        public void JumpIfTrue(int index, OptCode optCode, int firstParameter, int secondParameter, int result)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"JumpIfTrue on Index {index} with code {optCode}");

            var modeFirst = optCode.FirstParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"First: {firstParameter} [Mode:{modeFirst}]");

            var modeSecond = optCode.SecondParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Second: {secondParameter} [Mode:{modeSecond}]");

            builder.AppendLine($"Result: index {result}");

            Record(InstructionType.jumpIfTrue, builder);
        }

        public void LessThan(int index, OptCode optCode, int firstParameter, int secondParameter, int thirdParameter, int result)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"LessThan on Index {index} with code {optCode}");

            var modeFirst = optCode.FirstParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"First: {firstParameter} [Mode:{modeFirst}]");

            var modeSecond = optCode.SecondParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Second: {secondParameter} [Mode:{modeSecond}]");

            var modeThird = optCode.ThirdParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Result: {result} @ index {thirdParameter} [Mode:{modeThird}]");

            Record(InstructionType.lessThan, builder);
        }

        public void Multiplication(int index, OptCode optCode, int firstParameter, int secondParameter, int result, int destination)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Multiplication on Index {index} with code {optCode}");

            var modeFirst = optCode.FirstParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"First: {firstParameter} [Mode:{modeFirst}]");

            var modeSecond = optCode.SecondParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Second: {secondParameter} [Mode:{modeSecond}]");

            var modeThird = optCode.ThirdParameterIsPositionMode ? "position" : "immediate";
            builder.AppendLine($"Result: {result} @ index {destination} [Mode:{modeThird}]");

            Record(InstructionType.Multiplication, builder);
        }

        public void Output(int index, int valueIndex, int value)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Output on Index {index}");
            builder.AppendLine($"Value: {value} @ index {valueIndex}");

            Record(InstructionType.output, builder);
        }
    }
}
