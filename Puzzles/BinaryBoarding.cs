using System;

namespace Puzzles
{
    public static class BinaryBoarding
    {
        public static int[] GetSeat(string instructions)
        {
            var lowerBoundRow = 0;
            var upperBoundRow = 127;
            var lowerBoundColumn = 0;
            var upperBoundColumn = 7;
            foreach (var instruction in instructions)
            {
                var rowAdjustment = (upperBoundRow - lowerBoundRow) / 2 + 1;
                var columnAdjustment = (upperBoundColumn - lowerBoundColumn) / 2 + 1;
                switch (instruction)
                {
                    // lower half
                    case 'F':
                        upperBoundRow -= rowAdjustment;
                        break;
                    // upper half
                    case 'B':
                        lowerBoundRow += rowAdjustment;
                        break;
                    case 'R':
                        lowerBoundColumn += columnAdjustment;
                        break;
                    case 'L':
                        upperBoundColumn -= columnAdjustment;
                        break;
                }
            }

            if (lowerBoundRow != upperBoundRow || lowerBoundColumn != upperBoundColumn)
            {
                throw new NotSupportedException("WTF!");
            }

            var row = lowerBoundRow;
            var column = lowerBoundColumn;

            return new[] {row, column, row * 8 + column};
        }
    }
}