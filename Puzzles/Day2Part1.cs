using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles
{
    public class Day2Part1
    {
        public static int PasswordPhilosophy(IEnumerable<string> passwordList)
        {
            var validPasswords = 0;
            foreach (var passwordEntry in passwordList)
            {
                var parts = passwordEntry.Split(':');
                var password = parts.Last().Trim();
                var letter = Convert.ToChar(parts[0].Split(' ').Last());
                var boundSplit = parts[0].Split(' ').First()
                    .Split('-')
                    .Select(int.Parse)
                    .ToArray();
                var letterCount = password.Count(ch => ch == letter);
                if (boundSplit[0] <= letterCount && letterCount <= boundSplit[1]) validPasswords++;
            }

            return validPasswords;
        }
    }
}