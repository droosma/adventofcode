using System;
using System.Text;
using Xunit.Abstractions;

namespace Day05
{
    public class InMemoryComputationRecorder
    {
        private readonly Action<string> _output;
        private int _count = 0;

        private void Record(StringBuilder builder)
        {
            _count++;
            _output.Invoke($"command: {_count}{Environment.NewLine}{builder}");
        }

        private InMemoryComputationRecorder(Action<string> output)
        {
            _output = output;
        }

        public static InMemoryComputationRecorder ToConsole 
            => new InMemoryComputationRecorder(toOutput => Console.WriteLine(toOutput));
        public static InMemoryComputationRecorder ToTestOutputHelper(ITestOutputHelper helper) 
            => new InMemoryComputationRecorder(toOutput => helper.WriteLine(toOutput));

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

            Record(builder);
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

            Record(builder);
        }

        public void Failure(OptCode optCode)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Failed to execute program {optCode}");

            Record(builder);
        }

        public void Initialize(int[] source)
        {
            var builder = new StringBuilder();

            Record(builder);
            builder.AppendLine($"Initializing");
            builder.AppendLine(string.Join(',', source));

            Record(builder);
        }

        public void Input(int index, int firstParameter, int userInput)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Input on Index {index}");
            builder.AppendLine($"First: {firstParameter}");
            builder.AppendLine($"Set {userInput} @ index {firstParameter}");

            Record(builder);
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

            Record(builder);
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

            Record(builder);
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

            Record(builder);
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

            Record(builder);
        }

        public void Output(int index, int valueIndex, int value)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Output on Index {index}");
            builder.AppendLine($"Value: {value} @ index {valueIndex}");

            Record(builder);
        }
    }
}
