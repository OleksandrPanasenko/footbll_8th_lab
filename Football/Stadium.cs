using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    internal class Stadium
    {
        public static  StadiumList StadiumList=new StadiumList();
        private string NoName="Unnamed"; 
        public bool Unnamed
        {
            get { return name == NoName; }
        }
        public string name;
        public int number_of_seats;
        public float price_per_seat;
        public GamesList planned_games;
        public Stadium(string name) { 
            this.name = name;
            number_of_seats = 100;
            price_per_seat = 10;
            planned_games = new GamesList();
        }
        public Stadium()
        {
            this.name = "unnamed";
            number_of_seats = 100;
            price_per_seat = 10;
            planned_games = new GamesList();
        }
        public string Info
        {
            get
            {
                string toReturn = $"Name: {name}\n" +
                    $"Seats: {number_of_seats}\n" +
                    $"Price of ticket: {price_per_seat}\n" +
                    "Planned games:\n";
                planned_games.SortGames();
                foreach (Game game in planned_games.GetPlanned())
                {
                    toReturn += $"  {game.GameDate.DateWorld}: {game.Team1}-{game.Team2}\n";
                }
                return toReturn;
            }
        }

        public string to_file
        {
            get
            {
                string to_return = $"{this.name}|{number_of_seats}|{price_per_seat}|";

                to_return += "\n";
                return to_return;
            }
        }
        public static Stadium FromFileLine(string line)
        {
            if (StadiumList == null) Stadium.StadiumList = new StadiumList();
            string[] array = line.Split('|');
            Stadium stadium = new Stadium(array[0]);
            stadium.number_of_seats = int.Parse(array[1]);
            stadium.price_per_seat = float.Parse(array[2]);
            return stadium;
        }
        public void RemovePlannedGame(Game game)
        {
            if (planned_games.list.Contains(game))
            {
                planned_games.list.Remove(game);
            }
        }
        public void Delete()
        {
            StadiumList.Remove(this);
        }
    }
}
