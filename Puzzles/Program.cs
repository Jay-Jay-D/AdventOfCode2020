using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzles
{
    public class BinaryBoarding
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
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var maxId = 0;
            var instructions = File.ReadAllLines("boarding_passes.txt");
            var occupedSeats = new List<int>();
            foreach (var instruction in instructions)
            {
                var id = BinaryBoarding.GetSeat(instruction);
                occupedSeats.Add(id[2]);
                maxId = Math.Max(maxId, id[2]);
            }
            Console.WriteLine(maxId);
            var mySeat = Enumerable.Range(1, 1023).Except(occupedSeats)
                .Where(s => occupedSeats.Contains(s - 1) && occupedSeats.Contains(s + 1))
                .ToList();

        }
    }
}