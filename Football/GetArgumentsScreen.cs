using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    internal class GetArgumentsScreen
    {
        public string description;
        List<string> arguments;
        public GetArgumentsScreen(string[]questions) {
            Console.WriteLine(description);
            arguments = new List<string>();
            foreach (string question in questions)
            {
                arguments.Add(question);
            }
        }
        public string ArgLine()
        {
            string responces = "";
            for (int i = 0;i<arguments.Count;i++)
            {
                Console.WriteLine(arguments[i]);
                responces += Console.ReadLine();
                if (i < arguments.Count - 1) responces += "|";
            }
            return responces;
        }
    }
}
