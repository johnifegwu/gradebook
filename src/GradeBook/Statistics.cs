namespace GradeBook
{
    public class Statistics
    {
        private double avarage;

        private double high;

        private double low;

        public Statistics(double avarage, double high, double low)
        {
            this.avarage = avarage;
            this.high = high;
            this.low = low;
        }

        public double Avarage
        { 
            get
            {
                return this.avarage;
            }
            set
            {
                if(value >= 0)
                {
                    this.avarage = value;
                }
            }
        }

        public double High
        {
             get
            {
                return this.high;
            }
            set
            {
                if(value >= 0 && value <= 100)
                {
                    this.high = value;
                }
            }
        }

        public double Low
        {
             get
            {
                return this.low;
            }
            set
            {
                if(value >= 0 && value <= 100)
                {
                    this.low = value;
                }
            }
        }
    }
}