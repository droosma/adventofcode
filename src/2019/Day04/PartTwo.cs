using FluentAssertions;
using System.Linq;
using Xunit;

namespace Day04
{
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

                if (current > previous)
                    continue;

                if (current < previous)
                    return false;

                var group = Enumerable.Range(i, code.Length - i)
                                      .Select(i => code[i])
                                      .Any(c => groups.Contains(c));
                if (!group)
                    return false;
            }

            return true;
        }

        private static bool Check(string input)
        {
            var orderedInput = input.OrderBy(i => i);
            bool increase = Enumerable.SequenceEqual(input, orderedInput);
            if (!increase)
            {
                return false;
            }

            return input.GroupBy(c => c).Any(g => g.Count() == 2);
        }

        [Fact]
        public void FromInput()
        {
            const int min = 206938;
            const int max = 679128;

            var range = Enumerable.Range(min, max - min).Select(o => o.ToString());
            var results = range.Where(IsValidPassCode);
            var results2 = range.Where(Check);

            results2.Count().Should().Be(1133);
        }
    }
}
