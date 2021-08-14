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

        public void AddGrade(char grade)
        {
            switch(grade)
            {
                case 'A':
                this.AddGrade(90);
                break;

                case 'B':
                this.AddGrade(80);
                break;

                case 'C':
                this.AddGrade(70);
                break;

                case 'D':
                this.AddGrade(60);
                break;

                case 'P':
                this.AddGrade(50);
                break;

                case 'F':
                this.AddGrade(40);
                break;

                default:
                this.AddGrade(0);
                break;
            }
        }

        public void AddGrade(double grade)
        {
            if(grade >= 0 && grade <= 100){
                grades.Add(grade);
            }
            else
            {
               throw new ArgumentException($"Invalid {nameof(grade)}"); 
            }    
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

            switch(result.Avarage)
            {
                case var l when l >= 90:
                result.Letter = 'A';
                break;

                case var l when l >= 80:
                result.Letter = 'B';
                break;

                case var l when l >= 70:
                result.Letter = 'C';
                break;

                case var l when l >= 60:
                result.Letter = 'D';
                break;

                case var l when l >= 50:
                result.Letter = 'P';
                break;

                default:
                result.Letter = 'F';
                break;

            }

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

        public bool HasGrades()
        {
            if(grades.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClrGrades()
        {
            this.grades.Clear();
        }
    }
}