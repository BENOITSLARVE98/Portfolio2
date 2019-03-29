using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlarveBenoit_TimeTrackerApp
{
    class Utilities
    {
        public static int ValidateInt(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            int i;
            while (!int.TryParse(response, out i) || !Enumerable.Range(1, 5).Contains(i))
            {
                Console.WriteLine("Please enter a valid integer OR a valid Option");
                response = Console.ReadLine();

            }
            return i;
        }

        public static int ValidateSpecialInt4(string s)// int max)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            int i;

            while (!int.TryParse(response, out i) || !Enumerable.Range(1, 4).Contains(i))
            {
                Console.WriteLine(@"Invalid *_* GOTCHA, try again!!");
                response = Console.ReadLine();
                //Console.Clear();
            }
            return i;
        }

        public static int ValidateSpecialInt10(string s)// int max)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            int i;

            while (!int.TryParse(response, out i) || !Enumerable.Range(1, 10).Contains(i))
            {
                Console.WriteLine(@"Invalid *_* GOTCHA, try again!!");
                response = Console.ReadLine();
                //Console.Clear();
            }
            return i;
        }
        public static decimal ValidateDecimal(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            decimal d;
            while (!decimal.TryParse(response, out d))
            {
                Console.WriteLine("Please enter a valid decimal number...");
                response = Console.ReadLine();
            }
            return d;
        }
        public static double ValidateDouble(string s)
        {
            Console.WriteLine();
            string response = Console.ReadLine();
            double d;
            while (!double.TryParse(response, out d))
            {
                Console.WriteLine("Please enter a valid number(double)...");
                response = Console.ReadLine();
            }
            return d;
        }
        public static float ValidateFloat(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            float f;
            while (!float.TryParse(response, out f))
            {
                Console.WriteLine("Please enter a valid number(float)...");
                response = Console.ReadLine();
            }
            return f;
        }
        public static string ValidateString(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("Please enter a valid answer");
                response = Console.ReadLine();
            }
            return response;
        }
    }

    class Menu
    {
        public string Title { get; set; }
        private List<string> _items;
        public Menu()
        {
            Title = "Application";
            _items = new List<string>();
        }
        public Menu(params string[] items)
        {
            Title = "Application";
            _items = items.ToList();
        }
        public void AddMenuItem(string item)
        {
            _items.Add(item);
        }
        public void Display()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Title);
            Console.WriteLine("=============================");

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {_items[i]}");
            }

        }
    }
}

