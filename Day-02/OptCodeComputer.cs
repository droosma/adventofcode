using System.Linq;

namespace Day_02
{
    public static class OptCodeComputer
    {
        const int exit = 99;
        const int add = 1;
        const int multiply = 2;

        public static void Output(ref int[] input)
        {
            var index = 0;
            while ((index = Compute(ref input, index)) > 0) ;

            static int Compute(ref int[] source, int index)
            {
                var cache = source.Skip(index)
                                 .Take(4)
                                 .ToArray();

                var optCode = cache[0];

                if (optCode == exit) return -1;

                var noun = cache[1];
                var verb = cache[2];
                var destination = cache[3];

                if (optCode == add)
                {
                    var result = source[noun] + source[verb];
                    source[destination] = result;
                }
                else if (optCode == multiply)
                {
                    var result = source[noun] * source[verb];
                    source[destination] = result;
                }

                return index + 4;
            }
        }
    }
}
