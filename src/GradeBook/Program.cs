using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var book =  new Book("Mike's Grade Book");

            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);

            var stat = book.GetStats();

            Console.WriteLine($"The highest grade is {stat.High:N1}");
            Console.WriteLine($"The avarage grade is {stat.Avarage:N1}");
            Console.WriteLine($"The lowest grade is {stat.Low:N1}");

        }
    }
}
