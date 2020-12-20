using System.Linq;
using NUnit.Framework;
using Puzzles;

namespace Tests
{
    [TestFixture]
    public class BinaryBoardingTests
    {
        [TestCase("FBFBBFFRLR", new[] {44, 5, 357})]
        [TestCase("BFFFBBFRRR", new[] {70, 7, 567})]
        [TestCase("FFFBBBFRRR", new[] {14, 7, 119})]
        [TestCase("BBFFBBFRLL", new[] {102, 4, 820})]
        public void BinaryBoardingTest(string instructions, int[] expected)
        {
            Assert.AreEqual(expected,
                BinaryBoarding.GetSeat(instructions));
        }
    }


    [TestFixture]
    public class PassaportProcessingTests
    {
        [Test]
        public void ParsePassportTest()
        {
            var passportFile = "passport_testing.txt";
            var passportProcessor = new PassportProcessor(passportFile);
            Assert.AreEqual(4, passportProcessor.Passports.Count);
        }

        [Test]
        public void CheckPassportContainsObligatoryFieldsTest()
        {
            var passportFile = "passport_testing.txt";
            var passportProcessor = new PassportProcessor(passportFile);
            Assert.AreEqual(2, passportProcessor.CountFieldComplete());
        }

        [TestCase("passport_validity_test.txt", 4)]
        public void CheckPassportValidityTest(string passportFile, int expectedValid)
        {
            var passportProcessor = new PassportProcessor(passportFile);
            Assert.AreEqual(expectedValid, passportProcessor.CountValid());
        }
    }

    [TestFixture]
    public class Tests
    {
        [TestCase(new[] {1721, 979, 366, 299, 675, 1456}, new[] {1721, 299})]
        public void Day1Part1Test(int[] expenses, int[] expectedEntries)
        {
            var actualEntries = Puzzles.Day1Part1.ReportRepair(expenses);
            Assert.AreEqual(expectedEntries, actualEntries);
        }

        [TestCase(new[] {1721, 979, 366, 299, 675, 1456}, new[] {979, 366, 675})]
        public void Day1Part2Test(int[] expenses, int[] expectedEntries)
        {
            var actualEntries = Puzzles.Day1Part2.ReportRepair(expenses);
            Assert.AreEqual(expectedEntries, actualEntries);
        }

        [TestCase(new[] {"1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"}, 2)]
        public void Day2Part1Test(string[] passwordList, int validPasswords)
        {
            var actual = Puzzles.Day2Part1.PasswordPhilosophy(passwordList);
            Assert.AreEqual(validPasswords, actual);
        }

        [TestCase(new[] {"1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"}, 1)]
        public void Day2Part2Test(string[] passwordList, int validPasswords)
        {
            var actual = Puzzles.Day2Part2.PasswordPhilosophy(passwordList);
            Assert.AreEqual(validPasswords, actual);
        }
    }

    [TestFixture]
    public class TobogganTrajectoryTests
    {
        [Test]
        public void TobogganTrajectoryParseMap()
        {
            var tobogganTrajectory = new TobogganTrajectory("toboggan_testing_map.txt");
            Assert.AreEqual(11, tobogganTrajectory.MapHeight);
            Assert.AreEqual(11, tobogganTrajectory.MapWidth);
        }

        [TestCase(new[] {1, 3}, new[] {0, 0}, new[] {0, 0, 1, 3})]
        [TestCase(new[] {0, 2}, new[] {0, 10}, new[] {0, 0, 0, 3})]
        [TestCase(new[] {3, 4}, new[] {0, 11}, new[] {0, 0, 3, 4})]
        public void TobogganTrajectoryApplyRule(int[] expectedPosition, int[] initialPosition, int[] rule)
        {
            var tobogganTrajectory = new TobogganTrajectory("toboggan_testing_map.txt");
            var actualPosition = tobogganTrajectory.ApplyRule(initialPosition, rule);
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [Test]
        public void TobogganTrajectoryDetectTree()
        {
            var tobogganTrajectory = new TobogganTrajectory("toboggan_testing_map.txt");

            var actualPosition = tobogganTrajectory.ApplyRule(new[] {0, 0}, new[] {0, 0, 1, 3});
            Assert.IsFalse(tobogganTrajectory.IsTRee(actualPosition));

            actualPosition = tobogganTrajectory.ApplyRule(actualPosition, new[] {0, 0, 1, 3});
            Assert.IsTrue(tobogganTrajectory.IsTRee(actualPosition));
        }

        [TestCase(2, new[] {0, 0, 1, 1})]
        [TestCase(7, new[] {0, 0, 1, 3})]
        [TestCase(3, new[] {0, 0, 1, 5})]
        [TestCase(4, new[] {0, 0, 1, 7})]
        [TestCase(2, new[] {0, 0, 2, 1})]
        public void TobogganTrajectoryCountTrees(int expectedCount, int[] rule)
        {
            var tobogganTrajectory = new TobogganTrajectory("toboggan_testing_map.txt");

            var trees = tobogganTrajectory.CountTrees(rule);
            Assert.AreEqual(expectedCount, trees);
        }
    }
}