namespace  Football
{
    public class Date{
        public int Day;
        public int Month;
        public int Year;
        public Date(int day, int month, int year){
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }
        public Date(string strdate)
        {
            if (strdate == "" || strdate == " ")
            {
                this.Day = DateTime.Now.Day;
                this.Month = DateTime.Now.Month;
                this.Year = DateTime.Now.Year;
            }
            else
            {
                try
                {
                    string[] T = strdate.Split('/');
                    if (T.Length < 3)
                    {
                        T = strdate.Split(" ");
                    }
                    if (T.Length < 3)
                    {
                        T = strdate.Split("-");
                    }
                    if (T.Length < 3)
                    {
                        T = strdate.Split(".");
                    }
                    Day = int.Parse(T[0]);
                    if (Day < 0 || Day > 31) throw new ArgumentException("Day must be between 1 and 31!");
                    Month = int.Parse(T[1]);
                    if (Month < 0 || Month > 12) throw new ArgumentException("Month must be between 1 and 12!");
                    Year = int.Parse(T[2]);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public string DateWorld{
            get {return $"{Day}/{Month}/{Year}";}
        }
        public string DateFreedom{
            get {return $"{Month}/{Day}/{Year}";}
        }
        public static bool operator ==(Date a, Date b)
        {
            //if (a == null && b == null) return true;
            //if (a == null || b == null) return false;
            if(a.Day == b.Day&&a.Month==b.Month&&a.Year==b.Year) return true;
            return false;
        }
        public static bool operator !=(Date a, Date b)
        {
            return !(a == b);
        }
        public static bool operator >(Date a, Date b)
        {
            if(a==b) return false;
            //if (b == null) return true;
            //if(a == null) return false;
            if (a.Year != b.Year)
            {
                return a.Year>b.Year;
            }
            if(a.Month != b.Month)
            {
                return a.Month>b.Month;
            }
            if(a.Day != b.Day)
            {
                return a.Day>b.Day;
            }
            return false;
        }
        public static bool operator <(Date a, Date b)
        {
            return (a!=b)&&!(a>b);
        }
    }
}