using Football;
using System.Runtime.CompilerServices;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
FileSave.LoadConfiguration();
BaseInterface Starting = new BaseInterface("Choose an action among following:\n");

PlayerMode PlayerInterraction = new PlayerMode();
PlayerInterraction.AddAction("Go back", Starting.Display);
GameMode GameInterraction = new GameMode();
GameInterraction.AddAction("Go back", Starting.Display);
StadiumMode StadiumInterraction = new StadiumMode();
StadiumInterraction.AddAction("Go back", Starting.Display);
Starting.AddAction("Player mode", PlayerInterraction.Display);
Starting.AddAction("Game mode", GameInterraction.Display);
Starting.AddAction("Stadium mode", StadiumInterraction.Display);
Starting.AddAction("Save changes", () => { Console.WriteLine("Saving");FileSave.SaveConfiguration();Console.WriteLine("Saved! Press Enter");Console.ReadLine(); });
Starting.AddAction("Close", () => { FileSave.SaveConfiguration(); Starting.end = true; Environment.Exit(0); });
Starting.Display();
//BaseInterface GameMode = new BaseInterface("Interract with the game\n");
//BaseInterface StadiumMode = new BaseInterface("Choose what you want to find\n");
