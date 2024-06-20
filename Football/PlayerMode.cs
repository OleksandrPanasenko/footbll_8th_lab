using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class PlayerMode:BaseInterface
    {
        public PlayerMode() : base() {
            description = "You are interracting with players. What do you want to do?\n";
            this.AddAction("1. Add player\n", AddPlayer);
            this.AddAction("2. Show list of Players\n", ShowAllPlayers);
            this.AddAction("3. Find player\n", FindPlayer);
            this.AddAction("4. Delete Player\n",DeletePlayer);
        }
        private void AddPlayer()
        {
            Player NewPlayer = new Player();
            NewPlayer.Name = Validate.validate_string("Name:");
            NewPlayer.Surname = Validate.validate_string("Surame:");          
            NewPlayer.Status = Validate.validate_string("Status:");
            NewPlayer.HealthStatus = Validate.validate_string("Health Status:");
            NewPlayer.BirthDate = Validate.validate_date("Date of birth:");
            NewPlayer.Salary = (double)Validate.validate_float("Salary of player:");
            try
            {
                Player existing = Player.PlayerList[NewPlayer.Name + " " + NewPlayer.Surname];
                if (existing != null)
                {
                    Console.WriteLine("There already was said player! Press enter to procede\n");
                    Console.ReadLine();
                }
            }
            catch (ArgumentException)
            {
                Player.PlayerList.Add(NewPlayer);
            }
        }
        private void DeletePlayer()
        {
            if (Player.PlayerList == null || Player.PlayerList.list.Count == 0)
            {
                Console.WriteLine("There is no players! enter to procede\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Write the name and surname of the player you want to delete\n");
                try
                {
                    GetArgumentsScreen ask = new GetArgumentsScreen(["Name: ", "Surname: "]);
                    ask.description = "Input name and surname of player you want to find\n";
                    string[] FullName = ask.ArgLine().Split('|');
                    Player to_delete = Player.PlayerList[FullName[0] + " " + FullName[1]];
                    to_delete.Delete();
                    if (Game.gamesList != null)
                    {
                        foreach (Game game in Game.gamesList)
                        {
                            if (game.GamePlayers.list.Contains(to_delete))
                            {
                                game.GamePlayers.Remove(to_delete);
                            }
                        }
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("There was no player with such name!\nPress enter to proceed");
                    Console.ReadLine();
                }
            }
        }
        private void ShowAllPlayers()
        {
            BaseInterface actions_with_players = new BaseInterface();
            actions_with_players.description = Player.PlayerList != null&&Player.PlayerList.list.Count > 0?"choose a player to interract with":"there are no players";
            actions_with_players.AddAction("go back",this.Display);
            actions_with_players.escapeAction=this.Display;
            foreach(Player player in Player.PlayerList)
            {
                ActionsWithPlayer innermost = new ActionsWithPlayer(player);
                innermost.escapeAction = this.Display;
                innermost.AddAction("go back", this.Display);
                Action action_with_individual_player = (innermost).Display;
                actions_with_players.AddAction(player.Info, action_with_individual_player);

            }
            actions_with_players.Display();
        }
        private void FindPlayer()
        {
            bool end = true;
            GetArgumentsScreen ask = new GetArgumentsScreen(["Name: ", "Surname: "]);
            ask.description = "Input name and surname of player you want to find\n";
            Player found=new Player();
            try
            {
                string[]FullName=ask.ArgLine().Split('|');
                found = Player.PlayerList[FullName[0] + " " + FullName[1]];
                if (found == null) end = false;
            }
            catch
            {
                end=false;
                Console.WriteLine("There was no player with such name!\nPress enter to proceed");
                Console.ReadLine();
            }
            if (end)
            {
                ActionsWithPlayer menu= new ActionsWithPlayer(found);
                menu.escapeAction = this.Display;
                menu.AddAction("back", this.Display);
                menu.Display();
            }
        }
    }
    internal class ActionsWithPlayer: BaseInterface
    {
        Player Selected;
        public ActionsWithPlayer(Player selected_player):base()
        {
            description = selected_player.Info+"You can modify or delete player now, or add to existing game\n";
            Selected= selected_player;
            AddAction("1.Add player to game", AddToGame);
            AddAction("2.Modify player", ModifyPlayer);
            AddAction("3.Delete player", Delete);
        }
        private void Delete()
        {
            Selected.Delete();
            if (Game.gamesList != null)
            {
                foreach (Game game in Game.gamesList)
                {
                    game.GamePlayers.Remove(Selected);
                }
            }
            Console.WriteLine("succesfully deleted!\nEnter to procede");
            Console.ReadLine();
            escapeAction();
        }
        private void ModifyPlayer()
        {
            Console.Clear();
            description = Selected.Info + "You can modify or delete player now, or add to existing game\n(changes would be seen once you exit and reenter)";
            Console.WriteLine(description);
            ModifyPlayer modmenu=new ModifyPlayer(Selected);
            modmenu.escapeAction = this.Display;
            modmenu.AddAction("back", Display);
            modmenu.Display();
        }
        private void AddToGame()
        {
            Game chosen;
            GetArgumentsScreen askDate = new GetArgumentsScreen(["When played?\n", "Team"]);
            string[] data = askDate.ArgLine().Split('|');
            try
            {
                chosen = Game.gamesList[data[1], new Date(data[0])];
                chosen.GamePlayers.Add(Selected);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Addition unsuccesful. There was no such game\nPress enter to procede");
                Console.ReadLine();
            }
        }
    }
    internal class ModifyPlayer: BaseInterface { 
        Player Selected;
        internal ModifyPlayer(Player selected_player) : base()
        {
            Selected = selected_player;
            description = selected_player.Info+"Choose what you want to edit\n";
            this.AddAction("Edit name", ChangeName);
            this.AddAction("Edit surname", ChangeSurname);
            AddAction("Edit Status",ChangeStatus);
            AddAction("Edit Health Status", ChangeHealthStatus);
            AddAction("Edit date of Birth", ChangeBirthDate);
            AddAction("Edit salary", ChangeSalary);
        }
        private void ChangeName()
        {
            Selected.Name = Validate.validate_string("Name:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        private void ChangeSurname()
        {
            Selected.Surname = Validate.validate_string("Surname:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        private void ChangeStatus()
        {
            Selected.Status = Validate.validate_string("Status:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        private void ChangeHealthStatus()
        {
            Selected.HealthStatus = Validate.validate_string("Health Status:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        private void ChangeBirthDate()
        {
            Selected.BirthDate= Validate.validate_date("Date of birth:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
        private void ChangeSalary()
        {
            Selected.Salary = (double)Validate.validate_float("Salary of player:\n");
            description = Selected.Info + "Choose what you want to edit\n";
        }
    }
}
