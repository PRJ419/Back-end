using System;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new BarOMeterContext())
            {
                var bar = new Bar();
                bar.BarName = "Hejsa";
                bar.CVR = 12345678;
                bar.Address = "False address";
                bar.AgeLimit = 18;
                bar.AvgRating = 3;
                bar.Educations = "Ingen";
                bar.Email = "falskEmail@uni.dk";
                bar.PhoneNumber = 30257681;
                bar.LongDescription = "Lang beskrivelse";
                bar.ShortDescription = "Kort beskrivelse";

                context.Add(bar);
                context.SaveChanges();
            }

            
        }
    }
}
