using System;

namespace GradeBook
{
    public class Statistics
    {
        private double avarage;

        private double high;

        private double low;

        private char letter;

        private double sum;

        private int count;

        public Statistics(double avarage, double high, double low)
        {
            this.avarage = avarage;
            this.high = high;
            this.low = low;
            this.count = 0;
            this.sum = 0.0;
        }

        public void Add(double grade)
        {
            sum += grade;
            count += 1;
            this.high = Math.Max(grade, this.high);
            this.low = Math.Min(grade, this.low);
        }

        public double Avarage
        { 
            get
            {
                return sum / count;
            }
         
        }

        public double High
        {
             get
            {
                return this.high;
            }
            
        }

        public double Low
        {
             get
            {
                return this.low;
            }
           
        }

        public char Letter
        {
            get
            {
                switch (this.Avarage)
                {
                    case var l when l >= 90:
                        return 'A';
                       
                    case var l when l >= 80:
                        return 'B';
                       
                    case var l when l >= 70:
                        return 'C';
                       
                    case var l when l >= 60:
                        return 'D';
                       
                    case var l when l >= 50:
                       return 'P';
                       
                    default:
                        return 'F';
                       
                }

            }

        }
    }
}