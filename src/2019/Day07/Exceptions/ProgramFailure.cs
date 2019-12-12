using System;

namespace Day07
{
    internal class ProgramFailure : Exception
    {
        private ProgramFailure(OptCode optCode, int index) 
            : base($"Program failure unknown type `{optCode.Type}` given @ `{index}`")
        {

        }
        public static ProgramFailure Create(OptCode optCode, int index) => new ProgramFailure(optCode, index);
    }
}
