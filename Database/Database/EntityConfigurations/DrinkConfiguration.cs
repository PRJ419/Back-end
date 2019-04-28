using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class DrinkConfiguration : IEntityTypeConfiguration<Drink>
    {
        public void Configure(EntityTypeBuilder<Drink> builder)
        {
            builder
                .HasOne(d => d.Bar)
                .WithMany(b => b.Drinks)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.BarName);

            builder
                .HasKey(a => new { a.BarName, a.DrinksName });

            #region Dataseeding

            builder.HasData(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "Spejlæg",
                Price = 50
            });

            builder.HasData(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "Flaskeøl",
                Price = 10,
                Image = "https://www.calle.dk/SL/PI/705/128/8c021bde-d649-4515-8c92-0effa962bafe.jpg"
            });

            builder.HasData(new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "Fadøl",
                Price = 20,
                Image = "https://r2brewery.dk/wp-content/uploads/2017/11/pilsner.png"
            });

            builder.HasData(new Drink()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                DrinksName = "Ceres Top",
                Price = 10,
                Image = "https://www.calle.dk/SL/PI/705/128/8c021bde-d649-4515-8c92-0effa962bafe.jpg"
            });

            builder.HasData(new Drink()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                DrinksName = "Vodka Redbull",
                Price = 20,
                Image = "https://www.drinkdelivery.it/wp-content/uploads/2015/05/vodka-absolute-redbull.jpg"
            });

            builder.HasData(new Drink()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                DrinksName = "Hospitalssprit",
                Price = 10,
                Image = "https://www.fotoagent.dk/single_picture/11981/138/mega/201096_1.jpg"
            });

            #endregion
        }
    }
}
