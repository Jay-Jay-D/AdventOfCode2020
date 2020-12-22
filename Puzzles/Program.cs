using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzles
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var answers = "./InputData/answers.txt";
            Console.WriteLine($"{CustomCustoms.ReadAnswers(answers)}");
            Console.WriteLine($"{CustomCustoms.ReadAnswers(answers, false)}");
        }
    }
}