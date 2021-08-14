using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var book =  new Book("Mike's Grade Book");

            book.GradeAdded += OnGradeAdded;

            var desc = "\nEnter a grade in Letters or numbers 0 to 100 or R for result & Q to quit:";

            Console.WriteLine(desc);

            while(true)
            {
                string grade = Console.ReadLine();
                var x = double.MinValue;

                if(double.TryParse(grade, out x))
                {
                    try
                    {
                        book.AddGrade(x);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    

                }else if(grade != string.Empty)
                {

                    if(grade == "r" || grade == "R")
                    {
                        if(book.HasGrades())
                        {

                             var stat = book.GetStats();
                             try
                             {
                                // book.Name = "";
                                 Console.WriteLine($"The book is named {book.Name}");
                             }
                             catch(Exception ex)
                             {
                                 Console.WriteLine(ex.Message);
                             }
                             Console.WriteLine($"The highest grade is {stat.High:N1}");
                             Console.WriteLine($"The avarage grade is {stat.Avarage:N1}");
                             Console.WriteLine($"The lowest grade is {stat.Low:N1}");
                             Console.WriteLine($"The letter grade is {stat.Letter}");

                             book.ClrGrades();
                             Console.WriteLine(desc);

                        }
                       
                    }
                    else if(grade == "q" || grade == "Q")
                    {

                         break;

                    }
                    else if(grade.Length == 1)
                    {

                        char d;
                        char.TryParse(grade, out d);
                        book.AddGrade(d);

                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }

                }
            }

        }

        static void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("A grade was added.");
        }
    }
}
