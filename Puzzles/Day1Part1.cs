using System;

namespace Puzzles
{
    public class Day1Part1
    {
        public static int[] ReportRepair(int[] expenses)
        {
            for (var i = 0; i < expenses.Length - 1; i++)
            {
                var j = i;
                while (j <= expenses.Length - 2)
                {
                    if (i == ++j) continue;
                    if (expenses[i] + expenses[j] == 2020)
                    {
                        return new[] {expenses[i], expenses[j]};
                    }
                }
            }

            throw new NotSupportedException("I expect result");
        }
    }
}