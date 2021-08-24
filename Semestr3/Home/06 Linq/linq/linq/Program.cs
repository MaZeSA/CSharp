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
            var query1 =
            from city in cities
            where city.IsCapital == true
            select city;
            query1.ToList().ForEach(x => Console.WriteLine($"{x.Name} {x.Country} {x.Population}"));

            Console.WriteLine($"\nназви мiст, що мiстять букву 'i' у назвi мiста;");
            query1 =
            from city in cities
            where city.Name.IndexOf("i") >= 0
            select city;
            query1.ToList().ForEach(x => Console.WriteLine(x.Name));

            Console.WriteLine("\nназви столиць разом iз населенням у порядку спадання населення;");
            query1 =
            from city in cities
            where city.IsCapital == true
            orderby city.Population descending
            select city;
            query1.ToList().ForEach(x => Console.WriteLine(x.Name + " " + x.Population));

            Console.WriteLine("\nназви країн, що мiстять мiста, назви яких закiнчуютсья на букву 'w';");
            query1 =
            from city in cities
            where city.Name.Last() == 'w'
            select city;
            query1.ToList().ForEach(x => Console.WriteLine(x.Country));

            Console.WriteLine("\nназви мiст, де назва країни закiнчується на 'e', а назви мiст починаються на букву 'a';");
            query1 =
            from city in cities
            where city.Country.Last() == 'w' && city.Name.ToString().ToLower() == "a"
            select city;
            query1.ToList().ForEach(x => Console.WriteLine(x.Name));
            Console.WriteLine("Примiтка : В даному прикладi немає жодного мiста, яке починається на 'а'");

            Console.WriteLine("\nвсi непарнi числа;");
            var query2 =
            from num in numbers
            where num % 2 != 0
            select num;
            query2.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\n\nдодатнi числа iз 'numbers' у порядку зростання;");
            query2 =
            from num in numbers
            where num > 0
            orderby num
            select num;
            query2.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\n\nвiд'ємнi числа iз 'numbers' у порядку спадання.");
            query2 =
            from num in numbers
            where num < 0
            orderby num descending
            select num;
            query2.ToList().ForEach(x => Console.Write(x + " "));


            Console.WriteLine("\nкiлькiсть столиць;");
            Console.WriteLine(cities.Where(x => x.IsCapital == true).Count());

            Console.WriteLine("\nназви країн;");
            cities.ToList().ForEach(x => Console.WriteLine(x.Country));

            Console.WriteLine("\nкiлькiсть мiст iз населенням бiльше  1 000 000;");
            Console.WriteLine(cities.Where(x => x.Population > 1000000).Count());

            Console.WriteLine("\nназви мiст iз населенням менше  1 000 000;");
            cities.Where(x => x.Population < 1000000).ToList().ForEach(x => Console.WriteLine(x.Name));

            Console.WriteLine("\nназви країн, у яких назви мiст закiнчуютсья на букву 'w' у назвi мiста;");
            cities.Where(x => x.Name.Last() == 'w').ToList().ForEach(x => Console.WriteLine(x.Country));

            Console.WriteLine("\nкiлькiсть населення в найменш заселенiй столицi;");
            Console.WriteLine(cities.Where(x => x.IsCapital == true).OrderBy(x => x.Population).First().Population);

            Console.WriteLine("\nназви мiст, крiм перших i останнiх чотирьох;");
            int rang = 4;
            cities.ToList().GetRange(rang, cities.Count() - (rang + rang)).ForEach(x => Console.WriteLine(x.Name));

            Console.WriteLine("\nмiнiмальне, максимальне та середнє значення;");
            Console.WriteLine($"Min: {numbers.Min()} Avg: {numbers.Average()} Max: {numbers.Max()}");

            Console.WriteLine("\nчи мiстить масив значення '-31'");
            if (numbers.FirstOrDefault(x => x == -31) != default)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }

            Console.WriteLine("\nостаннє парне значення.");
            Console.WriteLine(numbers.Where(x => x % 2 == 0).Last());

            Console.ReadLine();
        }
    }
}