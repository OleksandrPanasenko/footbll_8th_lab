using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    internal class GamesList:IEnumerable
    {
        internal List<Game> list;
        public GamesList()
        {
            list = new List<Game>();
        }
        public void Add(Game Game)
        {
            list.Add(Game);
        }

        public IEnumerator GetEnumerator()
        {
            SortGames();
            foreach (Game Game in list)
            {
                yield return Game;
            };
        }
        public IEnumerable GetInfoByResult()
        {
            SortGames();
            yield return "Gamew won:\n";
            foreach (Game game in GetWon()) yield return game.Info;
            yield return "Games lost:\n";
            foreach (Game game in GetLost()) yield return game.Info;
            yield return "Games in draw:\n";
            foreach (Game game in GetDraw()) yield return game.Info;
            yield return "Games planned:\n";
            foreach (Game game in GetPlanned()) yield return game.Info;
        }
        public IEnumerable GetWon()
        {
            foreach(Game game in this)
            {
                if(game.Happenned&&game.Result==1) yield return game;
            }
        }
        public IEnumerable GetLost()
        {
            foreach (Game game in this)
            {
                if (game.Happenned&&game.Result == -1) yield return game;
            }
        }
        public IEnumerable GetDraw()
        {
            foreach (Game game in this)
            {
                if (game.Happenned && game.Result == 0) yield return game;
            }
        }
        public IEnumerable GetPlanned()
        {
            foreach (Game game in this)
            {
                if (!game.Happenned) yield return game;
            }
        }
        public IEnumerable GamesInfo()
        {
            SortGames();
            foreach (Game Game in list)
            {
                yield return Game.Info;
            }
        }
        public Game this[string opponent, Date date]
        {
            get
            {
                foreach (Game Game in list)
                {
                    if ((Game.Team1==opponent || Game.Team2 ==opponent) && Game.GameDate==date)
                    {
                        return Game;
                    }
                }
                throw new ArgumentException($"there is no such Game");
            }
        }
        public bool Sorted
        {
            get {
                
                for (int i = 0; i <list.Count-1;i++)
                {
                    if (list[i].GameDate < list[i + 1].GameDate) return false;
                }
                return true;
            }
        } 
        public void SortGames()
        {
            if (!Sorted)
            {
                for(int i = 1;i < list.Count;i++)
                {
                    for(int j = 0; j < i; j++)
                    {
                        if(list[j].GameDate < list[j + 1].GameDate)
                        {
                            Game buffer= list[j];
                            list[j] = list[j +1];
                            list[j +1] = buffer;
                        }
                    }
                }
            }
        }
        public void Remove(Game Game)
        {
            list.Remove(Game);
        }
    }
}
