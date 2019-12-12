using System;

namespace Day07
{
    internal class InputNotProvided : Exception
    {
        private InputNotProvided(int index) 
            : base($"Program requested input at `{index}` that was not provided")
        {

        }
        public static InputNotProvided Create(int index) => new InputNotProvided(index);
    }
}
