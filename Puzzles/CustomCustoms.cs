using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzles
{
    public class CustomCustoms
    {
        public static int ReadAnswers(string answersFile, bool anyone = true)
        {
            var answerCount = 0;
            using (var stream = File.OpenRead(answersFile))
            using (var reader = new StreamReader(stream))
            {
                var groupAnswers = new List<char>();
                var personCount = 0;
                var line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        // new group
                        answerCount += AnswerCount(groupAnswers, personCount, anyone);
                        groupAnswers.Clear();
                        personCount = 0;
                        continue;
                    }

                    groupAnswers.AddRange(line.ToCharArray());
                    personCount++;
                }

                groupAnswers.AddRange(line.ToCharArray());
                personCount++;
                answerCount += AnswerCount(groupAnswers, personCount, anyone);
            }

            return answerCount;
        }

        private static int AnswerCount(List<char> groupAnswers, int personCount, bool anyone)
        {
            if (anyone)
            {
                return groupAnswers.Distinct().Count();
            }
            else
            {
                var answerCount = 0;
                foreach (var question in groupAnswers.Distinct())
                {
                    answerCount += groupAnswers.Count(a => a == question) == personCount ? 1 : 0;
                }

                return answerCount;
            }
        }
    }
}