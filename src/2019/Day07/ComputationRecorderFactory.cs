using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Day07
{
    public static class ComputationRecorderFactory
    {
        public static ComputationRecorder ToComputationRecorder(this ITestOutputHelper outputHelper)
            => new ComputationRecorder((type, count, output) 
                => outputHelper.WriteLine($"command: {count}{Environment.NewLine}type: {type}{Environment.NewLine}{output}"));
        public static ComputationRecorder ToComputationRecorder(this IList<InstructionLog> instructionLogs) => new ComputationRecorder((type, count, output)
                  => instructionLogs.Add(new InstructionLog(count, type, output)));
    }
}
