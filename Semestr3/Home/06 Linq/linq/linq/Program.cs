using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq
{
    class Program
    {
        static City[] cities = new City[]
           {
              new City{ Name = "Rivne", Country="Ukraine", Population = 250000, IsCapital = false },
              new City{ Name = "Warsaw", Country="Poland", Population = 1800000, IsCapital = true },
              new City{ Name = "Lviv", Country="Ukraine", Population = 720000, IsCapital = false },
              new City{ Name = "Krakow", Country="Poland", Population = 780000, IsCapital = false },
              new City{ Name = "Odessa", Country="Ukraine", Population = 1020000, IsCapital = false },
              new City{ Name = "London", Country="Great Britain", Population = 8900000, IsCapital = true },
              new City{ Name = "Paris", Country="France", Population = 2180000, IsCapital = true },
              new City{ Name = "Berlin", Country="Germany", Population = 3600000, IsCapital = true },
              new City{ Name = "Wroclaw", Country="Poland", Population = 640000, IsCapital = false },
              new City{ Name = "Kyiv", Country="Ukraine", Population = 3000000, IsCapital = true },
              new City{ Name = "Munich", Country="Germany", Population = 1480000, IsCapital = false },
              new City{ Name = "Dnipro", Country="Ukraine", Population = 980000, IsCapital = false },
              new City{ Name = "Cologne", Country="Germany", Population = 1000000, IsCapital = false }
           };
        static int[] numbers = new[] { 2, 9, 47, 69, 20, -1, 13, -26, 37, -40, 18, 70, -31, 7, -47, -7, 1 };
        static void Main(string[] args)
        {
            Console.WriteLine($"iнформацiю про столицi(назву, країну та населення);");
            cities.Where(x=> x.IsCapital == true).ToList().ForEach(x => Console.WriteLine($"{x.Name}  {x.Country} {x.Population}"));

            Console.WriteLine($"\nназви мiст, що мiстять букву 'i' у назвi мiста;");
            cities.Where(x => x.Name.IndexOf("i") >= 0).ToList().ForEach(x => Console.WriteLine(x.Name));

            Console.WriteLine("\nназви столиць разом iз населенням у порядку спадання населення;");
            cities.Where(x => x.IsCapital == true).ToList().OrderByDescending(x => x.Population).ToList().ForEach(x => Console.WriteLine(x.Name + " " + x.Population));

            Console.WriteLine("\nназви країн, що мiстять мiста, назви яких закiнчуютсья на букву 'w';");
            cities.Where(x => x.Name.Last() == 'w').ToList().ForEach(x => Console.WriteLine(x.Country));

            Console.WriteLine("\nназви мiст, де назва країни закiнчується на 'e', а назви мiст починаються на букву 'a';");
            cities.Where(x => x.Country.Last() == 'e' && x.Name.First().ToString().ToLower() == "a" ).ToList().ForEach(x => Console.WriteLine(x.Name));
            Console.WriteLine("Примiтка : В даному прикладi немає жодного мiста, яке починається на 'а'");

            Console.WriteLine("\nвсi непарнi числа;");
            numbers.Where(x => x % 2 != 0).ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\n\nдодатнi числа iз 'numbers' у порядку зростання;");
            numbers.Where(x => x > 0).ToList().OrderBy(x => x).ToList().ForEach(x => Console.Write(x + " "));
           
            Console.WriteLine("\n\nвiд'ємнi числа iз 'numbers' у порядку спадання.");
            numbers.Where(x => x < 0).ToList().OrderByDescending(x => x).ToList().ForEach(x => Console.Write(x + " "));

            Console.ReadLine();
        }
    }
}