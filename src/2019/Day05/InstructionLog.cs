namespace Day05
{
    public class InstructionLog
    {
        public int Index { get; }
        public InstructionType Type { get; }
        public string Output { get; }

        public InstructionLog(int index,
                              InstructionType type,
                              string output)
        {
            Index = index;
            Type = type;
            Output = output;
        }
    }
}
