using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGames
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new FirstContext();

            //var game = new Games
            //{
            //    NameGame = "Cossacks",
            //    Date = new DateTime(2002, 8, 18),
            //    Studio = "GSC",
            //    Style = "Strategy"
            //}; 
            //var game2 = new Games
            //{
            //    NameGame = "S.T.A.L.K.E.R",
            //    Date = new DateTime(2007, 3, 20),
            //    Studio = "GSC Game World, THQ",
            //    Style = "Survival horror"
            //};
            //context.Games.Add(game); 
            //context.Games.Add(game2);

            //context.SaveChanges();

            foreach(var g in context.Games)
            {
                Console.WriteLine($"{ g.NameGame} {g.Studio} {g.Style} {g.Date}");
            }

            Console.ReadLine();
        }
    }
}
