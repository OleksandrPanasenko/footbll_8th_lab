using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    internal abstract class Validate
    {
        internal static int validate_int(string message)
        {
            if (message == "") { return 0; }
            bool end = false;
            int to_return = 0;
            while (!end)
            {
                try
                {
                    Console.WriteLine(message);
                    to_return = int.Parse(Console.ReadLine());
                    end = true;
                }
                catch { 
                    Console.WriteLine("Number must be integer! ");
                }
            }
            return to_return;
        }
        internal static float validate_float(string message)
        {
            if (message == "") { return 0; }
            bool end = false;
            float to_return = 0;
            while (!end)
            {
                try
                {
                    Console.WriteLine(message);
                    to_return = float.Parse(Console.ReadLine());
                    end = true;
                }
                catch
                {
                    Console.WriteLine("Number must be float! ");
                }
            }
            return to_return;
        }
        internal static Date validate_date(string message)
        {
            bool end = false;
            Date to_return=new Date("01/01/01");
            while (!end)
            {
                try
                {
                    Console.WriteLine(message);
                    to_return = new Date(Console.ReadLine());
                    end = true;
                }
                catch
                {
                    Console.WriteLine("Date should be of format dd/mm/yyyy! ");
                }
            }
            return to_return;
        }
        internal static string validate_string(string message)
        {
            bool end = false;
            string to_return="";
            while (!end)
            {
                Console.WriteLine(message);
                to_return = Console.ReadLine();
                if (!to_return.Contains('|'))
                {
                    end = true;
                }
                else
                {
                    Console.WriteLine("Don't use symbol '|' in strings");
                }
            }
            return to_return;
        }
    }
}
