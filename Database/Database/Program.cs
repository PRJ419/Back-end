using System;
using System.Collections.Generic;
using System.Linq;
using Database.Redundancy;
using Database.Repository_Implementations;
using Microsoft.AspNetCore.Hosting;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Back-end. Muhahahaha!");
            var context = new BarOMeterContext();
            using (var uow = new UnitOfWork())
            {
                //var bar = new Bar();
                //bar.BarName = "ABar";
                //bar.Address = "hejsaAddress";
                //bar.AgeLimit = 21;
                //bar.AvgRating = 3;
                //bar.CVR = 11111115;
                //bar.Email = "ksdalfjads";
                //bar.PhoneNumber = 1234;
                //uow.BarRepository.Add(bar);
                //uow.Complete();
                var yyy = uow.DrinkRepository.Get("Testbar", "drinksnavn");

                //Console.WriteLine("{0}",yyy.DrinksName);

                //var xxx = uow.Bars.GetBestBars();
                //var drink = uow.DrinkRepository.Find(x=>x.DrinksName == "drinksnavn" && x.BarName == "Testbar");
                //foreach (var VARIABLE in xxx)
                //{
                //    Console.WriteLine("{0}", VARIABLE.BarName);
                //}
                //uow.BarRepository.Delete("aaaaaa");
                //uow.Complete();

                var ppp = uow.Bars.GetXBars(1, 0);
                foreach (var VARIABLE in ppp)
                {
                    Console.WriteLine("{0}", VARIABLE.BarName);   
                }
            }

            Console.WriteLine("Over and out fra databasen");
            var wait = Console.ReadLine();
        }
    }
}
