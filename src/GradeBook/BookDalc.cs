using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace GradeBook.Dalc
{
    public class BookDalc
    {

        protected readonly string conString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = gradebook; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False";

        public void SaveBook(Book book)
        {
            if (book.HasGrades() && !string.IsNullOrEmpty(book.Name))
            {
                //Create Factory
                SqlClientFactory factory = SqlClientFactory.Instance; //DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");

                //Create Database Connection
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = conString;

                //Create Database Command
                DbCommand bookCommand = factory.CreateCommand();
                bookCommand.CommandText = "INSERT INTO books (Id, bookname) VALUES(NEXT VALUE FOR book_seq, @bookname)";
                bookCommand.CommandType = CommandType.Text;
                bookCommand.Connection = connection;

                DbParameter nameParameter = factory.CreateParameter();
                nameParameter.ParameterName = "@bookname";
                nameParameter.DbType = DbType.String;
                nameParameter.Direction = ParameterDirection.Input;
                nameParameter.Value = book.Name;

                bookCommand.Parameters.Add(nameParameter);

                try
                {
                    connection.Open();
                    if (bookCommand.ExecuteNonQuery() > 0)
                    {
                        //Save grades to the system
                        foreach (double grade in book.Grades)
                        {
                            //Create Database Command
                            DbCommand gradeCommand = factory.CreateCommand();
                            gradeCommand.CommandText = "INSERT INTO [dbo].[grades] ([Id], [bookname], [grade]) VALUES (NEXT VALUE FOR grade_seq, @bookname, @grade)";
                            gradeCommand.CommandType = CommandType.Text;
                            gradeCommand.Connection = connection;

                            DbParameter bookParameter = factory.CreateParameter();
                            bookParameter.ParameterName = "@bookname";
                            bookParameter.DbType = DbType.String;
                            bookParameter.Direction = ParameterDirection.Input;
                            bookParameter.Value = book.Name;

                            DbParameter gradeParameter = factory.CreateParameter();
                            gradeParameter.ParameterName = "@grade";
                            gradeParameter.DbType = DbType.Double;
                            gradeParameter.Direction = ParameterDirection.Input;
                            gradeParameter.Value = grade;

                            gradeCommand.Parameters.Add(bookParameter);
                            gradeCommand.Parameters.Add(gradeParameter);

                            gradeCommand.ExecuteNonQuery();
                        }

                        Console.WriteLine($"Grade book saved successfully.");
                        book.ClrGrades();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                Console.Write($"Can not save empty Grade Book to the system.");
            }

        }

        public Book GetBook(string studentName)
        {
            //Create Factory
            SqlClientFactory factory = SqlClientFactory.Instance; //DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");

            //Create Database Connection
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = conString;

            //Create Database Command
            DbCommand bookCommand = factory.CreateCommand();
            bookCommand.CommandText = "SELECT Id, bookname, grade FROM grades WHERE (bookname = @bookname)";
            bookCommand.CommandType = CommandType.Text;
            bookCommand.Connection = connection;

            DbParameter nameParameter = factory.CreateParameter();
            nameParameter.ParameterName = "@bookname";
            nameParameter.DbType = DbType.String;
            nameParameter.Direction = ParameterDirection.Input;
            nameParameter.Value = studentName;

            bookCommand.Parameters.Add(nameParameter);

            Book book = null;

            try
            {

                connection.Open();
                DbDataReader dr = bookCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    if (book == null)
                    {
                        book = new Book((string)dr.GetValue("bookname"));
                    }
                    book.AddGrade(Convert.ToDouble(dr.GetValue("grade")));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return book;

        }

    }
}
