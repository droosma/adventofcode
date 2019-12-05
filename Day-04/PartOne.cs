using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Day_04
{
    public class PartOne
    {
        [Fact]
        public void FromExample()
        {
            const int min = 206938;
            const int max = 679128;

            var options = new List<int>();
            for (int option = min; option <= max; option++)
            {
                if (IsValidPassCode(option.ToString()))
                    options.Add(option);
            }

            static bool IsValidPassCode(string code)
            {
                if (code.GroupBy(n => n).All(n => n.Count() < 2))
                    return false;

                var last = code.First();
                foreach (var digit in code.Skip(1))
                {
                    if (digit < last)
                        return false;

                    last = digit;
                }

                return true;
            }

            options.Count().Should().Be(1653);
        }
    }
}
