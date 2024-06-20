using System.Xml.Linq;

namespace Football{
    public class Player{
        internal static Players PlayerList=new Players();
        public string Name;
        public string Surname;
        public Date BirthDate;
        public string Status;
        public string HealthStatus;
        public double Salary;

        public string Info{
            get{
                return $"Full name: {Name} {Surname}\n"+
                $"Was born: {BirthDate.DateWorld}\n"+
                $"Status: {Status}\n"+
                $"Health condition: {HealthStatus}\n"+
                $"Salary: ${Salary}\n";
            }
        }
        public Player(string name, string surname, Date birthDate, string status, string healthStatus, double salary){
            Name = name;
            Surname= surname;
            BirthDate = birthDate;
            Status = status;
            HealthStatus = healthStatus;
            Salary = salary;
        }
        public Player(string name, string surname, Date birthDate){
            Name = name;
            BirthDate = birthDate;
            Surname= surname;
            Status="-";
            HealthStatus="-";
            Salary=0;
        }
        public Player(string name, string surname){
            Name = name;
            Surname=surname;
            BirthDate=new Date(0,0,0);
            Status="-";
            HealthStatus="-";
            Salary=0;
        }
        public Player(){
            Name = "name";
            Surname="surname";
            BirthDate=new Date(0,0,0);
            Status="-";
            HealthStatus="-";
            Salary=0;
        }
        public void Delete(){
            if (PlayerList.list.Contains(this))
            {
                PlayerList.Remove(this);
            }
        }

        public string to_file
        {
            get
            {
                string to_return = $"{this.Name}|{Surname}|{BirthDate.DateWorld}|{Status}|{HealthStatus}|{Salary}\n";
                return to_return;
            }
        }
        public static Player FromFileLine(string line)
        {
            string[] array = line.Split('|');
            Player player = new Player(array[0], array[1], new Date(array[2]), array[3],array[4],(double)float.Parse(array[5]));
            PlayerList.Add(player);
            return player;
        }
    }
}