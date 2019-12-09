namespace Day_05
{
    public class OptCode
    {
        public int Instruction { get; }
        public bool FirstParameterIsPositionMode { get; }
        public bool SecondParameterIsPositionMode { get; }
        public bool ThirdParameterIsPositionMode { get; }

        public OptCode(int code)
        {
            string format = "00000.##";
            var input = code.ToString(format);

            Instruction = int.Parse(input.Substring(3, 2));
            FirstParameterIsPositionMode = input[2].ToString() == "0" ? true : false;
            SecondParameterIsPositionMode = input[1].ToString() == "0" ? true : false;
            ThirdParameterIsPositionMode = input[0].ToString() == "0" ? true : false;
        }
    }
}
