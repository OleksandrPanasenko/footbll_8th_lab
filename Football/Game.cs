using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    internal class Game
    {
        public static Stadium noName = new Stadium("-");
        internal static GamesList gamesList=new GamesList();
        public Date GameDate;
        public Stadium Place;
        public int ViewersNumber;
        public bool Happenned;
        public int ScoreFirst;
        public int ScoreSecond;
        public string Team1;
        public string Team2;
        public Players GamePlayers=new Players();
        public Game()
        {
            GameDate = new Date(0, 0, 0);
            ViewersNumber = 0;
            Place = noName;
            Happenned = false;
            ScoreFirst = 0;
            ScoreSecond = 0;
            Team1 = "defender";
            Team2 = "attacker";
        }
        public int Result
        {
            get {
                if (ScoreFirst == ScoreSecond) return 0;
                else if (ScoreFirst > ScoreSecond) return 1;
                else return -1;
            }
        }
        public string Info
        {
            get
            {
                string information = "";
                if(Happenned)
                {
                    information += "Occurred, ";
                }
                else
                {
                    information += "Planned on ";
                }
                information += $"Date: {GameDate.DateWorld}\n" +
                $"{Team1} vs. {Team2}\n"+
                $"Place: {Place.name}\n";
                if (Happenned)
                {
                    switch (Result){
                        case 1:
                            { information += "The game was won with the \n";
                                break;
                            }
                        case 0:
                            { information += "The game ended with draw with the \n";
                                break;
                            }
                        default:
                            {
                                information += "The game was lost with the \n";
                                break;
                            }
                        }
                    information+= $"Score: {ScoreFirst}:{ScoreSecond} - ";
                }
                if (!Happenned)
                {
                    information += "Expected ";
                }
                information+=$"Number of viewers: {ViewersNumber}\n";
                return information;
            }
        }
        public string to_file
        {
            get
            {
                string to_return= $"{GameDate.DateWorld}|{Place.name}|{ViewersNumber}|{Happenned}|";
                if (Happenned)
                {
                    to_return += $"{ScoreFirst}|{ScoreSecond}|";
                }
                else
                {
                    to_return += $"0|0|";
                }
                to_return += $"{Team1}|{Team2}|";
                foreach(Player player in GamePlayers)
                {
                    to_return += $"{player.Name} {player.Surname}|";
                }
                to_return += "\n";
                return to_return;
            }
        }
        public static Game FromFileLine(string line)
        {
            Game game = new Game();
            string[] array = line.Split('|');

            game.GameDate = new Date(array[0]);
            try
            {
                game.Place = Stadium.StadiumList[array[1]];
            } catch (ArgumentException) { 
                Stadium added=new Stadium(array[1]);
                Stadium.StadiumList.Add(added);
                game.Place=added;
            }
            game.ViewersNumber = int.Parse(array[2]);
            game.Happenned = bool.Parse(array[3]);
            game.ScoreSecond = int.Parse(array[4]);
            game.ScoreSecond = int.Parse(array[5]);
            game.Team1 = array[6];
            game.Team2 = array[7];
            for(int i = 8; i < array.Length; i++)
            {
                if (array[i] != "")
                {
                    try
                    {
                        game.GamePlayers.Add(Player.PlayerList[array[i]]);
                    }
                    catch (ArgumentException)
                    {
                        if (array[i] != "")
                        {
                            string[] name = array[i].Split(" ");
                            Player added = new Player();
                            added.Name = name[0];
                            added.Surname = name[1];
                            Player.PlayerList.Add(added);
                            game.GamePlayers.Add(added);
                        }
                    }
                }
            }
            game.Place.planned_games.Add(game);
            Game.gamesList.Add(game);
            return game;
        }
        public void Delete()
        {
            if (gamesList.list.Contains(this))
            {
                gamesList.Remove(this);
            }
        }
    }
}
