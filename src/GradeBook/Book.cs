using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace GradeBook
{

    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        private string name;

        public NamedObject(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
                else
                {
                    throw new Exception($"Invalid name {value}");
                }

            }
        }

    }
    public class Book : NamedObject
    {

        public event GradeAddedDelegate GradeAdded;

        private List<double> grades;

        public Book(string name) : base(name)
        {
            grades = new List<double>();
        }

        public List<double> Grades
        {
            get{return grades;}
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
                // Raise Grade Added Event here..
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
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
                result.Add(grade);
            }

            return result;

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