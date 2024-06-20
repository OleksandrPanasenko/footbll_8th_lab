using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    
    internal class FileSave
    {
        internal static string This_path
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory();
            }
        }
        public static void SaveConfiguration()
        {
            SavePlayers();
            SaveStadiums();
            SaveGames();
        }
        public static void LoadConfiguration() {
            LoadStadiums();
            LoadPlayers();
            LoadGames();
        }
        private static void SaveStadiums() {
            StreamWriter file = new StreamWriter(File.Create(This_path + "\\Stadiums.txt"));
            foreach(Stadium stadium in Stadium.StadiumList)
            {
                file.Write(stadium.to_file);
            }
            file.Close();
        }
        private static void LoadStadiums()
        {
            if (File.Exists(This_path + "\\Stadiums.txt"))
            {
                StreamReader file = new StreamReader(This_path + "\\Stadiums.txt");
                string Str;
                while ((Str = file.ReadLine()) != null && Str != "")
                {
                    Stadium.StadiumList.Add(Stadium.FromFileLine(Str));
                }
                file.Close();
            }
            
        }
        private static void SavePlayers() {
            StreamWriter file = new StreamWriter(File.Create(This_path + "\\Players.txt"));
            foreach (Player player in Player.PlayerList)
            {
                file.Write(player.to_file);
            }
            file.Close();
        }
        private static void LoadPlayers() {
            if (File.Exists(This_path + "\\Players.txt"))
            {
                StreamReader file = new StreamReader(This_path + "\\Players.txt");
                string Str;
                while ((Str = file.ReadLine()) != null && Str != "")
                {
                    Player.FromFileLine(Str);
                }
                file.Close();
            }
        }
        private static void SaveGames() {
            StreamWriter file = new StreamWriter(File.Create(This_path + "\\Games.txt"));
            foreach (Game game in Game.gamesList)
            {
                file.Write(game.to_file);
            }
            file.Close();
        }
        private static void LoadGames() {
            if(File.Exists(This_path + "\\Games.txt"))
            {
                StreamReader file = new StreamReader(This_path + "\\Games.txt");
                string Str;
                while((Str=file.ReadLine()) != null&&Str!="") { 
                    Game.FromFileLine(Str);
                }
                file.Close ();
            }
        }
    }
    
}
