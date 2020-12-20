using System.IO;
using System.Linq;

namespace Puzzles
{
    public class TobogganTrajectory
    {
        public char[][] Map { get; set; }
        public int MapHeight { get; set; }
        public int MapWidth { get; set; }


        public TobogganTrajectory(string mapFilePath)
        {
            Map = ParseMap(mapFilePath);
            MapHeight = Map.Length;
            MapWidth = Map[0].Length;
        }

        private char[][] ParseMap(string mapFilePath)
        {
            var rows = File.ReadAllLines(mapFilePath);
            return rows.Select(t => t.ToCharArray()).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">[row, column]</param>
        /// <param name="rule">[left, up, down, right]</param>
        /// <returns>[row, column] after applying the rule</returns>
        public int[] ApplyRule(int[] position, int[] rule)
        {
            return new[]
            {
                position[0] - rule[1] + rule[2],
                (position[1] - rule[0] + rule[3]) % MapWidth
            };
        }

        public bool IsTRee(int[] actualPosition)
        {
            return Map[actualPosition[0]][actualPosition[1]] == '#';
        }

        public int CountTrees(int[] rule)
        {
            var treeCount = 0;
            var actualPosition = new[] {0, 0};
            do
            {
                actualPosition = ApplyRule(actualPosition, rule);
                if (IsTRee(actualPosition)) treeCount++;
            } while (actualPosition[0] < MapHeight - 1);

            return treeCount;
        }
    }
}