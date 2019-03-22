using System;
using System.Collections.Generic;
using System.Linq;
using Database.Repository_Implementations;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var context = new BarOMeterContext();
            using (var uow = new UnitOfWork(context))
            {
                var gr = new GenericRepository<Bar>(context);
                //var bar = new Bar();
                //bar.BarName = "nybar";
                //bar.Address = "address";
                //bar.AgeLimit = 18;
                //bar.AvgRating = 3;
                //bar.CVR = 12345678;
                //bar.Educations = "ingen";
                //bar.Email = "email";
                //bar.LongDescription = "Lang";
                //bar.ShortDescription = "kort";
                //bar.PhoneNumber = 12345678;
                //gr.Add(bar);

                var enumer = gr.Get("nybar");
                Console.WriteLine("{0}", enumer.BarName);

                var enumer2 = gr.Get("Testbar");

                Console.WriteLine("{0}", enumer2.BarName);
                //var test = gr.GetAll();
                //var bars = test.ToList();
                //foreach (var bar1 in bars)
                //{
                //    Console.WriteLine($"indgang: {bar1.BarName}");
                //}
                //var enumerator = test.GetEnumerator().Current;
                //foreach (var variable in test)
                //{
                //    Console.WriteLine("{0}",variable.BarName);
                //}

                uow.Complete();



                //Console.WriteLine("{0}", value.Current.BarName);
            }

            Console.WriteLine("Hejsa");
            var wait = Console.ReadLine();
        }
    }
}
