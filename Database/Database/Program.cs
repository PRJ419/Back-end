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
            using (var uow = new UnitOfWork())
            {
                var drink = new Drink();
                drink.BarName = "bbb";
                drink.DrinksName = "MeterPeter";
                drink.Price = 15.95;
                uow.DrinkRepository.Edit(drink);
                uow.Complete();

                var yyy = uow.BarRepository.GetBestBars();
                foreach (var VARIABLE in yyy)
                {
                    Console.WriteLine("{0}",VARIABLE.BarName);
                }


            }

            Console.WriteLine("Over and out fra databasen");
            var wait = Console.ReadLine();
        }
    }
}
