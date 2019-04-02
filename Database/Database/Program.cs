using System;
using System.Collections.Generic;
using System.Linq;
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
                //Opretter ny bar
                //var bar = new Bar();
                //bar.BarName = "Testbar"; // Skal være unik
                //bar.Address = "Adresse";
                //bar.AgeLimit = 18;
                //bar.AvgRating = 3.4;
                //bar.CVR = 12345678; // Skal være unik
                //bar.Educations = "Tilhørende Udd.";
                //bar.Email = "FalskEmail"; // Skal være unik
                //bar.PhoneNumber = 88888888;
                //bar.LongDescription = "LangBeskrivelse";
                //bar.ShortDescription = "KortBeskrivelse";

                // Tilføjer til databasen og gemmer det
                //uow.BarRepository.Add(bar);
                //uow.Complete();

                //var bruger = new Customer();
                //bruger.Username = "FalskBruger3";
                //bruger.DateOfBirth = new DateTime(1997, 05, 02);
                //bruger.Email = "FakeMail3";
                //bruger.Name = "Andy";

                //uow.CustomerRepository.Add(bruger);
                //uow.Complete();

                //var Event = new BarEvent();
                //Event.BarName = "Testbar";
                //Event.Date = new DateTime(2019, 05, 05);
                //Event.EventName = "Eventnavn";
                //uow.BarEventRepository.Add(Event);
                uow.Complete();
                

                var yyy = uow.BarRepository.Get("Testbar");
                
                Console.WriteLine("{0}",yyy.AvgRating);

                var ppp = uow.BarEventRepository.GetAll();
                foreach (var VARIABLE in ppp)
                {
                    Console.WriteLine("{0}",VARIABLE.EventName);
                }


                //var qqq = uow.CustomerRepository.Get("nyBruger");
                //var ppp = uow.CustomerRepository.Get("Username");
                //foreach (var VARIABLE in ppp.Reviews)
                //{
                //    Console.WriteLine("{0}", VARIABLE.BarPressure.ToString());
                //}


            }

            Console.WriteLine("Over and out fra databasen");
            var wait = Console.ReadLine();
        }
    }
}
