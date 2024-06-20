using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class StadiumMode : BaseInterface
    {
        public StadiumMode() : base()
        {
            description = "You are interracting with Stadiums. What do you want to do?\n";
            this.AddAction("1. Add Stadium\n", AddStadium);
            this.AddAction("2. Show list of Stadiums\n", ShowAllStadiums);
            this.AddAction("3. Find Stadium\n", FindStadium);
            this.AddAction("4. Delete Stadium\n", DeleteStadium);
        }
        private void AddStadium()
        {
            Stadium NewStadium = new Stadium();
            ModifyStadium set = new ModifyStadium(NewStadium);
            set.ChangeName();
            set.ChangeNumberOfSeats();
            set.ChangePrice();
            try
            {
                Stadium existing = Stadium.StadiumList[NewStadium.name];
                if (existing != null)
                {
                    Console.WriteLine("One team can't play two Stadiums at the same day! Press enter to procede\n");
                    Console.ReadLine();
                }
            }
            catch (ArgumentException)
            {
                Stadium.StadiumList.Add(NewStadium);
            }
        }
        private void DeleteStadium()
        {
            if (Stadium.StadiumList == null || Stadium.StadiumList.list.Count == 0)
            {
                Console.WriteLine("There is no Stadiums! enter to procede\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Write one of the teams and date of the Stadium you want to delete\n");
                try
                {
                    string name = Validate.validate_string("Name one of stadium");
                    Stadium to_delete = Stadium.StadiumList[name];
                    to_delete.Delete();
                    if (Game.gamesList != null)
                    {
                        foreach (Game game in Game.gamesList)
                        {
                            if (game.Place == to_delete)
                            {
                                game.Place = new Stadium();
                            }
                        }
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("There was no Stadium with such name!\nPress enter to proceed");
                    Console.ReadLine();
                }
            }
        }
        private void ShowAllStadiums()
        {
            BaseInterface actions_with_Stadiums = new BaseInterface();
            actions_with_Stadiums.description = Stadium.StadiumList != null && Stadium.StadiumList.list.Count > 0 ? "choose a Stadium to interract with" : "there are no Stadiums";
            actions_with_Stadiums.AddAction("go back", this.Display);
            actions_with_Stadiums.escapeAction = this.Display;
            if (Stadium.StadiumList != null)
            {
                foreach (Stadium Stadium in Stadium.StadiumList)
                {
                    ActionsWithStadium innermost = new ActionsWithStadium(Stadium);
                    innermost.escapeAction = this.Display;
                    innermost.AddAction("go back", this.Display);
                    Action action_with_individual_Stadium = (innermost).Display;
                    actions_with_Stadiums.AddAction(Stadium.Info, action_with_individual_Stadium);

                }
            }
            actions_with_Stadiums.Display();
        }
        private void FindStadium()
        {
            bool end = true;
            string name = Validate.validate_string("Name of stadium you want to find");
            Stadium found = new Stadium();
            try
            {
                found = Stadium.StadiumList[name];
                if (found == null) end = false;
            }
            catch
            {
                end = false;
                Console.WriteLine("There was no Stadium with such name!\nPress enter to proceed");
                Console.ReadLine();
            }
            if (end)
            {
                ActionsWithStadium menu = new ActionsWithStadium(found);
                menu.escapeAction = this.Display;
                menu.AddAction("back", this.Display);
                menu.Display();
            }
        }
    }
    internal class ActionsWithStadium : BaseInterface
    {
        Stadium Selected;
        public ActionsWithStadium(Stadium selected_Stadium) : base()
        {
            description = selected_Stadium.Info + "You can modify or delete Stadium now, or add to existing Stadium\n";
            Selected = selected_Stadium;
            AddAction("1.Add Game to Stadium", AddToStadium);
            AddAction("2.Modify Stadium", ModifyStadium);
            AddAction("3.See list of planned games (to interract with)",ShowPlannedGames);
            AddAction("4.See list of all games (by date)",ListByDate);
            AddAction("5.See list of games by outcomes",ListByOutcome);
            AddAction("6.Delete Stadium", Delete);
        }
        private void Delete()
        {
            Selected.Delete();
            if (Game.gamesList != null)
            {
                foreach (Game game in Game.gamesList)
                {
                    game.Place=new Stadium();
                }
            }
            Console.WriteLine("succesfully deleted!\nEnter to procede");
            Console.ReadLine();
            escapeAction();
        }
        private void ModifyStadium()
        {
            Console.Clear();
            description = Selected.Info + "You can modify or delete Stadium now, or add to existing Stadium\n(changes would be seen once you exit and reenter)";
            Console.WriteLine(description);
            ModifyStadium modmenu = new ModifyStadium(Selected);
            modmenu.escapeAction = this.Display;
            modmenu.AddAction("back", Display);
            modmenu.Display();
        }
        private void AddToStadium()
        {
            try
            {
                Date date = Validate.validate_date("When was the game played?");
                string team = Validate.validate_string("Name one of teams");
                Game to_add = Game.gamesList[team, date];
                Selected.planned_games.Add(to_add);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Addition unsuccesful. Was error {ex.Message}\nPress enter to procede");
                Console.ReadLine();
            }
        }
        private void ShowPlannedGames()
        {
            BaseInterface actions_with_Games = new BaseInterface();
            actions_with_Games.description = Game.gamesList != null && Game.gamesList.list.Count > 0 ? "choose a Game to interract with" : "there are no Games";
            actions_with_Games.AddAction("go back", this.Display);
            actions_with_Games.escapeAction = this.Display;
            if (Selected.planned_games != null)
            {
                foreach (Game Game in Selected.planned_games)
                {
                    if (!Game.Happenned)
                    {
                        ActionsWithGame innermost = new ActionsWithGame(Game);
                        innermost.escapeAction = this.Display;
                        innermost.AddAction("go back", this.Display);
                        Action action_with_individual_Game = (innermost).Display;
                        actions_with_Games.AddAction(Game.Info, action_with_individual_Game);
                    }
                }
            }
            actions_with_Games.Display();
        }
        private void ListByDate()
        {
            Console.WriteLine("Press enter to proceed\n");
            foreach (string game in Selected.planned_games.GamesInfo())
            {
                Console.WriteLine(game+"\n");
            }
            Console.ReadLine();
            
        }
        private void ListByOutcome()
        {
            Console.WriteLine("Press enter to proceed\n");
            foreach (string game in Selected.planned_games.GetInfoByResult())
            {
                Console.WriteLine(game + "\n");
            }
            Console.ReadLine();
        }
    }
    internal class ModifyStadium : BaseInterface
    {
        Stadium Selected;
        internal ModifyStadium(Stadium selected_Stadium) : base()
        {
            Selected = selected_Stadium;
            description = selected_Stadium.Info + "Choose what you want to edit\n";
            this.AddAction("Edit name", ChangeName);
            this.AddAction("Edit number of seats", ChangeNumberOfSeats);
            AddAction("Edit price per seat", ChangePrice);
        }
        internal void ChangeName()
        {
            Selected.name = Validate.validate_string("Name of stadium:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        
        internal void ChangeNumberOfSeats()
        {
            Selected.number_of_seats = Validate.validate_int("How many viewers can the Stadium fit:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        internal void ChangePrice()
        {
            Selected.price_per_seat = Validate.validate_float("Price of one-seat ticket:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
    }
}
