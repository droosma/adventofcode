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

            int Compute(ref int[] source, int index)
            {
                var cache = source.Skip(index)
                                 .Take(4)
                                 .ToArray();

                var optCode = cache[0];

                if (optCode == add)
                {
                    var first = cache[1];
                    var second = cache[2];
                    var destination = cache[3];

                    var result = source[first] + source[second];
                    source[destination] = result;
                }
                else if (optCode == multiply)
                {
                    var first = cache[1];
                    var second = cache[2];
                    var destination = cache[3];

                    var result = source[first] * source[second];
                    source[destination] = result;
                }
                else if (optCode == exit)
                {
                    return -1;
                }

                return index + 4;
            }
        }
    }
}
