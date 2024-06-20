using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class GameMode : BaseInterface
    {
        public GameMode() : base()
        {
            description = "You are interracting with Games. What do you want to do?\n";
            this.AddAction("1. Add Game\n", AddGame);
            this.AddAction("2. Show list of Games\n", ShowAllGames);
            this.AddAction("3. Find Game\n", FindGame);
            this.AddAction("4. Show text games sorted by date\n", ListByDate);
            this.AddAction("5. Show text games by categories\n", ListByOutcome);
            this.AddAction("6. Delete Game\n", DeleteGame);
        }
        private void AddGame()
        {
            Game NewGame = new Game();
            ModifyGame set=new ModifyGame(NewGame);
            set.ChangeDate();
            set.ChangePlace();
            set.ChangeNumberOfViewers();
            set.ChangeHappened();
            set.FirstTeamName();
            set.SecondTeamName();
            try
            {
                Game check = Game.gamesList[NewGame.Team1, NewGame.GameDate];
                Console.WriteLine("One team can't play two games at the same day! Press enter to procede\n");
                Console.ReadLine();
            }
            catch(ArgumentException)
            {
                try
                {
                    Game check = Game.gamesList[NewGame.Team2, NewGame.GameDate];
                    Console.WriteLine("One team can't play two games at the same day! Press enter to procede\n");
                    Console.ReadLine();
                }
                catch(ArgumentException)
                {
                    if (NewGame.Place != null && (Stadium.StadiumList.list.Contains(NewGame.Place) || NewGame.Place == Game.noName))
                    {
                        Game.gamesList.Add(NewGame);
                        NewGame.Place.planned_games.Add(NewGame);
                    }
                }
            }
        }
        private void DeleteGame()
        {
            if (Game.gamesList == null || Game.gamesList.list.Count == 0)
            {
                Console.WriteLine("There is no Games! enter to procede\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Write one of the teams and date of the Game you want to delete\n");
                try
                {
                    Date date = Validate.validate_date("When was the game played?");
                    string team = Validate.validate_string("Name one of teams");
                    Game to_delete = Game.gamesList[team, date];
                    to_delete.Delete();
                    if (Game.gamesList != null)
                    {
                        foreach (Stadium stadium in Stadium.StadiumList)
                        {
                            stadium.RemovePlannedGame(to_delete);
                        }
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("There was no Game with such name!\nPress enter to proceed");
                    Console.ReadLine();
                }
            }
        }
        private void ShowAllGames()
        {
            BaseInterface actions_with_Games = new BaseInterface();
            actions_with_Games.description = Game.gamesList != null && Game.gamesList.list.Count > 0 ? "choose a Game to interract with" : "there are no Games";
            actions_with_Games.AddAction("go back", this.Display);
            actions_with_Games.escapeAction = this.Display;
            if (Game.gamesList != null && Game.gamesList.list.Count > 0)
            {
                foreach (Game Game in Game.gamesList)
                {
                    ActionsWithGame innermost = new ActionsWithGame(Game);
                    innermost.escapeAction = this.Display;
                    innermost.AddAction("go back", this.Display);
                    Action action_with_individual_Game = (innermost).Display;
                    actions_with_Games.AddAction(Game.Info, action_with_individual_Game);

                }
            }
            else
            {
                Console.WriteLine("There are no games.Enter to proceed");
                Console.ReadLine();
            }
            actions_with_Games.Display();
        }
        private void FindGame()
        {
            if (Game.gamesList != null && Game.gamesList.list.Count > 0)
            {
                bool end = true;
                Date date = Validate.validate_date("When was the game played?");
                string team = Validate.validate_string("Name one of teams");
                Game found = new Game();
                try
                {
                    found = Game.gamesList[team, date];
                    if (found == null) end = false;
                }
                catch
                {
                    end = false;
                    Console.WriteLine("There was no game with such team and date!\nPress enter to proceed");
                    Console.ReadLine();
                }
                if (end)
                {
                    ActionsWithGame menu = new ActionsWithGame(found);
                    menu.escapeAction = this.Display;
                    menu.AddAction("back", this.Display);
                    menu.Display();
                }
            }
            else
            {
                Console.WriteLine("There are no games.Enter to proceed");
                Console.ReadLine();
            }
        }
        private void ListByDate()
        {
            if (Game.gamesList != null && Game.gamesList.list.Count > 0)
            {
                Console.WriteLine("Press enter to proceed\n");
                foreach (string game in Game.gamesList.GamesInfo())
                {
                    Console.WriteLine(game + "\n");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("There are no games.Enter to proceed");
                Console.ReadLine();
            }
            

        }
        private void ListByOutcome()
        {
            if (Game.gamesList != null && Game.gamesList.list.Count > 0)
            {
                Console.WriteLine("Press enter to proceed\n");
                foreach (string game in Game.gamesList.GetInfoByResult())
                {
                    Console.WriteLine(game + "\n");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("There are no games.Enter to proceed");
                Console.ReadLine();
            }
        }

    }
    internal class ActionsWithGame : BaseInterface
    {
        Game Selected;
        public ActionsWithGame(Game selected_Game) : base()
        {
            description = selected_Game.Info + "You can modify or delete Game now, or add to existing game\n";
            Selected = selected_Game;
            AddAction("1.Add Player to game", AddToGame);
            AddAction("2.Modify Game", ModifyGame);
            AddAction("3.Delete Game", Delete);
        }
        private void Delete()
        {
            Selected.Delete();
            if (Game.gamesList != null)
            {
                foreach (Stadium stadium in Stadium.StadiumList)
                {
                    stadium.RemovePlannedGame(Selected);
                }
            }
            Console.WriteLine("succesfully deleted!\nEnter to procede");
            Console.ReadLine();
            escapeAction();
        }
        private void ModifyGame()
        {
            Console.Clear();
            description = Selected.Info + "You can modify or delete Game now, or add to existing game\n(changes would be seen once you exit and reenter)";
            Console.WriteLine(description);
            ModifyGame modmenu = new ModifyGame(Selected);
            modmenu.escapeAction = this.Display;
            modmenu.AddAction("back", Display);
            modmenu.Display();
        }
        private void AddToGame()
        {
            GetArgumentsScreen ask = new GetArgumentsScreen(["Name: ", "Surname: "]);
            ask.description = "Input name and surname of player you want to add to the game\n";
            string[] FullName = ask.ArgLine().Split('|');
            try
            {

                Player to_add = Player.PlayerList[FullName[0] + " " + FullName[1]];
                Selected.GamePlayers.Add(to_add);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Addition unsuccesful. Was error {ex.Message}\nPress enter to procede");
                Console.ReadLine();
            }
        }
    }
    internal class ModifyGame : BaseInterface
    {
        Game Selected;
        internal ModifyGame(Game selected_Game) : base()
        {
            Selected = selected_Game;
            description = selected_Game.Info + "Choose what you want to edit\n";
            this.AddAction("Edit date", ChangeDate);
            this.AddAction("Edit place", ChangePlace);
            AddAction("Edit Number of viewers", ChangeNumberOfViewers);
            AddAction("Edit results", ChangeHappened);
            AddAction("Edit name of first team", FirstTeamName);
            AddAction("Edit name of second team", SecondTeamName);
        }
        internal void ChangeDate()
        {
            Selected.GameDate = Validate.validate_date("Date of playing:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void ChangePlace()
        {
            string place = Validate.validate_string("Stadium where the game took place:\n");
            if(place == null||place=="") {
                Selected.Place = Game.noName; ;
            }
            try
            {
                Selected.Place = Stadium.StadiumList[place];
                description = Selected.Info + "Choose what you want to edit\n";
            }
            catch (ArgumentException ex) {
                Console.WriteLine(ex.Message + ". Add stadium first\nPress enter to procede");
                Selected.Place = Game.noName;
            }
        }
        internal void ChangeNumberOfViewers()
        {
            Selected.ViewersNumber = Validate.validate_int("How many viewers saw the game:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void ChangeHappened()
        {
            int num = Validate.validate_int("Did the game happen? even number (0) - no, odd (1) - yes:\n");
            Selected.Happenned = num % 2 == 1;
            if(Selected.Happenned)
            {
                ChangeScoreFirst();
                ChangeScoreSecond();
            }
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void ChangeScoreFirst()
        {
            Selected.ScoreFirst = Validate.validate_int("First team scored:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void ChangeScoreSecond()
        {
            Selected.ScoreSecond = Validate.validate_int("Second team scored:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void FirstTeamName()
        {
            Selected.Team1= Validate.validate_string("First team name:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void SecondTeamName()
        {
            Selected.Team2 = Validate.validate_string("Second team name:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
    }
}
