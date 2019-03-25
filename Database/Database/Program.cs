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
            var context = new BarOMeterContext();
            using (var uow = new UnitOfWork(context))
            {
                var gr = new Repository<Bar>(context);
                var bar = new Bar();
                //bar.BarName = "Testbar";
                //bar.Address = "address";
                //bar.AgeLimit = 18;
                //bar.AvgRating = 3;
                //bar.CVR = 12345679;
                //bar.Educations = "ingen";
                //bar.Email = "email1";
                //bar.LongDescription = "Lang";
                //bar.ShortDescription = "kort";
                //bar.PhoneNumber = 12345678;
                //gr.Add(bar);

                var enumer = gr.Get("nybar");
                Console.WriteLine("{0}", enumer.BarName);

                var enumer2 = gr.Get("Testbar");

                Console.WriteLine("{0}", enumer2.BarName);
                var test = gr.GetAll();
                Console.WriteLine("Barer i databasen:");
                int i = 0;
                foreach (var bar1 in test)
                {
                    i++;
                    Console.WriteLine($"{i}: {bar1.BarName}");
                }
                //var enumerator = test.GetEnumerator().Current;
                //foreach (var variable in test)
                //{
                //    Console.WriteLine("{0}",variable.BarName);
                //}

                uow.Complete();



                //Console.WriteLine("{0}", value.Current.BarName);
            }

            Console.WriteLine("Over and out fra databasen");
            var wait = Console.ReadLine();
        }
    }
}
