using System;

namespace Puzzles
{
    public class Day1Part2
    {
        public static int[] ReportRepair(int[] expenses)
        {
            for (var i = 0; i < expenses.Length - 1; i++)
            {
                var j = i;
                while (j <= expenses.Length - 2)
                {
                    if (i == ++j) continue;
                    var h = j;
                    while (h <= expenses.Length - 3)
                    {
                        if (j == ++h) continue;
                        if (expenses[i] + expenses[j] + expenses[h] == 2020)
                        {
                            return new[] {expenses[i], expenses[j], expenses[h]};
                        }
                    }
                }
            }

            throw new NotSupportedException("I expect result");
        }
    }
}