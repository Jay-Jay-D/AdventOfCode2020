using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles
{
    public class Day2Part2
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
                    .Select(s => int.Parse(s) - 1)
                    .ToArray();
                if (password[boundSplit[0]] == letter ^ password[boundSplit[1]] == letter) validPasswords++;
            }

            return validPasswords;
        }
    }
}