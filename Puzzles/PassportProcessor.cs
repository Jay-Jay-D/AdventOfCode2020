using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzles
{
    public class PassportProcessor
    {
        private static readonly string[] ValidEyeColor = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        private readonly Dictionary<string, Func<string, bool>> _fieldValidityRules =
            new Dictionary<string, Func<string, bool>>
            {
                {
                    "byr", s =>
                    {
                        if (!int.TryParse(s, out var year)) return false;
                        return 1920 <= year && year <= 2002;
                    }
                },
                {
                    "iyr", s =>
                    {
                        if (!int.TryParse(s, out var year)) return false;
                        return 2010 <= year && year <= 2020;
                    }
                },
                {
                    "eyr", s =>
                    {
                        if (!int.TryParse(s, out var year)) return false;
                        return 2020 <= year && year <= 2030;
                    }
                },
                {
                    "hgt", s =>
                    {
                        if (!int.TryParse(s.Substring(0, s.Length - 2), out var height))
                        {
                            return false;
                        }

                        var unit = s.Substring(s.Length - 2);

                        switch (unit)
                        {
                            case "cm":
                                return 150 <= height && height <= 193;
                            case "in":
                                return 59 <= height && height <= 76;
                            default:
                                return false;
                        }
                    }
                },
                {
                    "hcl", s => s[0] == '#' &&
                                s.Skip(1).All(c => ('0' <= c && c <= '9') || ('a' <= c && c <= 'f'))
                },
                {"ecl", s => ValidEyeColor.Contains(s)},
                {"pid", s => s.Length == 9 && s.All(char.IsDigit)},
                {"cid", s => true}
            };

        private string[] _obligatoryFields;

        public List<Dictionary<string, string>> Passports { get; set; }

        public PassportProcessor(string passportFile)
        {
            Passports = ParsePassports(passportFile);
            _obligatoryFields = _fieldValidityRules.Keys.Where(f => f != "cid").ToArray();
        }

        private List<Dictionary<string, string>> ParsePassports(string passportFile)
        {
            var passports = new List<Dictionary<string, string>>();
            var passportString = new List<string>();
            using (var stream = File.OpenRead(passportFile))
            using (var reader = new StreamReader(stream))
            {
                var passportContent = string.Empty;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        passportString.Add(passportContent.Trim());
                        passportContent = string.Empty;
                        continue;
                    }

                    passportContent += $" {line}";
                }

                passportString.Add(passportContent.Trim());
            }

            foreach (var passportStr in passportString)
            {
                var entry = passportStr.Split(' ')
                    .Select(e => e.Split(':'))
                    .ToDictionary(kvp => kvp[0], kvp => kvp[1]);
                passports.Add(entry);
            }

            return passports;
        }

        public int CountFieldComplete()
        {
            return Passports.Count(HasRequiredFields);
        }

        public int CountValid()
        {
            return Passports.Count(IsValid);
        }

        private bool IsValid(Dictionary<string, string> passport)
        {
            return HasRequiredFields(passport) && CheckFields(passport);
        }

        private bool CheckFields(Dictionary<string, string> passport)
        {
            return passport.All(field => _fieldValidityRules[field.Key](field.Value));
        }

        private bool HasRequiredFields(Dictionary<string, string> passport)
        {
            return !_obligatoryFields.Except(passport.Keys).Any();
        }
    }
}