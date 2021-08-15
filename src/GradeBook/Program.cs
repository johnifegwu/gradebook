using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {

            //Create Book
            while(true)
            {
                Book book;

                Console.WriteLine($"Type 'New' to begin, 'S' to search or 'Q' to quit.");
                var str = Console.ReadLine();

                if(!string.IsNullOrEmpty(str))
                {
                    if(str.ToLower() == "new")
                    {
                        while(true)
                        {
                            Console.WriteLine($"Enter Student's Name or Q to quit:");
                            var input = Console.ReadLine();

                            if (!string.IsNullOrEmpty(input) && input.ToLower() != "q")
                            {
                                book = new Book(input);
                                book.GradeAdded += OnGradeAdded;
                                
                                var desc = "\nEnter a grade in Letters or numbers 0 to 100 or R for result & Q to quit:";

                                Console.WriteLine(desc);

                                while (true)
                                {
                                    string grade = Console.ReadLine();
                                    var x = double.MinValue;

                                    if (double.TryParse(grade, out x))
                                    {
                                        try
                                        {
                                            book.AddGrade(x);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }


                                    }
                                    else if (grade != string.Empty)
                                    {

                                        if (grade == "r" || grade == "R")
                                        {
                                            if (book.HasGrades())
                                            {

                                                PrintBookSummary(book);

                                                Console.WriteLine(desc);

                                            }

                                        }
                                        else if (grade == "q" || grade == "Q")
                                        {
                                            Console.WriteLine("Saving Grades to the database, please wait....");
                                            Dalc.Dalc.book().SaveBook(book);
                                            break;

                                        }
                                        else if (grade.Length == 1)
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
                            else if(!string.IsNullOrEmpty(input) && input.ToLower() == "q")
                            {
                                break;
                            }

                        }
                    }    
                    else if(str.ToLower() == "s")
                    {
                        while(true)
                        {
                            Console.WriteLine($"Enter a Student Name to search or 'Q' to quit:");
                            var text = Console.ReadLine();
                            if (!string.IsNullOrEmpty(text) && text.ToLower() != "q")
                            {
                                Console.WriteLine("Fetching Grades from the database, please wait....");
                                var bk = Dalc.Dalc.book().GetBook(text);
                                if(bk != null)
                                {
                                    Console.WriteLine($"Grade for {bk.Name}:");
                                    foreach(double grade in bk.Grades)
                                    {
                                        Console.WriteLine($"{grade:N1}");
                                    }
                                    PrintBookSummary(bk);
                                }
                                else
                                {
                                    Console.WriteLine("Record not found...");
                                }
                            }
                            else if(!string.IsNullOrEmpty(text) && text.ToLower() == "q")
                            {
                                break;
                            }
                        }

                    }
                    else if(str.ToLower() == "q")
                    {
                        break;
                    }
                }
            }

        }

        static void PrintBookSummary(Book book)
        {
            var stat = book.GetStats();
            try
            {
                // book.Name = "";
                Console.WriteLine($"The book is named {book.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"The highest grade is {stat.High:N1}");
            Console.WriteLine($"The avarage grade is {stat.Avarage:N1}");
            Console.WriteLine($"The lowest grade is {stat.Low:N1}");
            Console.WriteLine($"The letter grade is {stat.Letter}\n\n\n");
        }

        static void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("A grade was added.");
        }

        static Book GetBook(string bookName)
        {
            var book = new Book(bookName);
            
            try
            {

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return book;
        }
    }
}
