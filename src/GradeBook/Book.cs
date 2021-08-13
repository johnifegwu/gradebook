using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class Book
    {
        private List<double> grades;
        private string name;

        public Book(string name)
        {
            grades = new List<double>();
            this.name = name;
        }

        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public Statistics GetStats()
        {
            var result = new Statistics(0.0, double.MinValue, double.MaxValue);
           
            foreach(var grade in grades)
            {
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
                result.Avarage +=  grade;

            }

            result.Avarage /= grades.Count;

            return result;

        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if(value != string.Empty)
                   this.name = value;
            }
        }
    }
}