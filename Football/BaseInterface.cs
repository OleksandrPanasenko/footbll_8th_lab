using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class BaseInterface
    {
        public bool end;
        public string description;
        public int NumberOptions;
        public int current_option;
        public Action escapeAction;
        public List<Action> actions;
        public List<string> action_descriptions;
        public BaseInterface() {
            description = "";
            actions = new List<Action>();
            action_descriptions = new List<string>();
            current_option = 0;
            NumberOptions = 0;
        }
        public BaseInterface(string description):this()
        {
            this.description = description;
        }
        public void AddAction(string name, Action action)
        {
            actions.Add(action);
            action_descriptions.Add(name);
            NumberOptions++;
        }
        public void Display()
        {
            end= false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine(description);
                for (int i = 0; i < actions.Count; i++)
                {
                    if (i != current_option)
                    {
                        Console.WriteLine("    " + action_descriptions[i]);
                    }
                    else Console.WriteLine("--->" + action_descriptions[i]);
                }
                switch (Console.ReadKey(false).Key)
                {
                    case (ConsoleKey)38:
                        {
                            HandleUp(); break;
                        }
                    case (ConsoleKey)40:
                        {
                            HandleDown(); break;
                        }
                    case (ConsoleKey)27:
                        {
                            end = true;
                            if (escapeAction != null)
                            {
                                escapeAction.Invoke();
                            }
                            break;
                        }
                    case (ConsoleKey)13:
                        {
                            actions[current_option].Invoke(); break;
                        }
                    default:
                        {
                            Display(); break;
                        }
                };
            }
        }
        private void HandleUp()
        {
            current_option--;
            if (current_option < 0)
            {
                current_option = NumberOptions - 1;
            }
        }
        private void HandleDown()
        {
            current_option++;
            if (current_option >= NumberOptions)
            {
                current_option = 0;
            }
        }

    }
}
