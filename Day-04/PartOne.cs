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
                    if (digit >= last)
                    {
                        last = digit;
                        continue;
                    }

                    return false;
                }

                return true;
            }

            options.Count().Should().Be(1653);
        }
    }

    public class PartTwo
    {
        [Theory]
        [InlineData("112233", true)]
        [InlineData("123444", false)]
        [InlineData("111122", true)]
        public void FromExample(string input, bool expectedResult)
        {
            var result = IsValidPassCode(input);
            result.Should().Be(expectedResult);
        }

        private static bool IsValidPassCode(string code)
        {
            var groups = code.GroupBy(n => n)
                             .Where(n => n.Count() == 2)
                             .Select(n => n.Key);

            if (!groups.Any())
                return false;
            
            var largerGroups = code.GroupBy(n => n)
                                   .Where(n => n.Count() > 2)
                                   .Select(n => n.Key);

            for (int i = 1; i < code.Length; i++)
            {
                var current = code[i];
                var previous = code[i - 1];
                //var next = code[i + 1];

                if (current < previous)
                    return false;

                if(current == previous)
                {
                    var range = Enumerable.Range(i, code.Length - i);
                    if (range.Count() > 1)
                    {
                        var gelijkOfGroter = range.Select(i => code[i])
                                                  .All(c => c >= current);
                        if (!gelijkOfGroter)
                            return false;
                    }
                    else if (!groups.Contains(current))
                        return false;
                }
            }

            return true;
        }

        [Fact]
        public void FromInput()
        {
            const int min = 206938;
            const int max = 679128;

            var range = Enumerable.Range(min, max - min).Select(o => o.ToString());
            var result = range.Where(IsValidPassCode).Count();
        }
    }
}
