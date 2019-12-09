using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Day02
{
    public class PartTwo
    {
        [Fact]
        public void FromInput()
        {
            const int expectedOutput = 19690720;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "input.txt";

            var input = Utilities.EmbeddedResource.Get(resourceName, assembly);

            var optCodes = input.Split(",")
                                .Select(int.Parse)
                                .ToArray();

            var nouns = Enumerable.Range(0, 99);
            var verbs = Enumerable.Range(0, 99);

            Parallel.ForEach(nouns, noun =>
            {
                Parallel.ForEach(verbs, verb =>
                {
                    var cache = new int[optCodes.Length];
                    System.Array.Copy(optCodes, cache, optCodes.Length);
                    cache[1] = noun;
                    cache[2] = verb;

                    OptCodeComputer.Output(ref cache);

                    if (cache[0] == expectedOutput)
                        System.Console.WriteLine($"waaaa noun {noun} - verb {verb} result = {100 * noun + verb}");
                });
            });
        }
    }
}
