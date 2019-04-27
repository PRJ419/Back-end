using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class BarConfiguration : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder
                .HasKey(b => b.BarName);

            builder
                .HasIndex(b => b.CVR)
                .IsUnique();

            builder
                .HasIndex(b => b.Email)
                .IsUnique();

            #region Dataseeding

            builder.HasData(new Bar()
            {
                BarName = "Katrines Kælder",
                Address = "5125 Edison, Finlandsgade 22, 8200 Aarhus",
                AgeLimit = 18,
                Educations = "IKT,EE,E,ST",
                ShortDescription = "Der er øl",
                LongDescription = "Der er mange øl",
                CVR = 33985703,
                PhoneNumber = 12345678,
                Email = "katrineskaelder@outlook.dk",
                AvgRating = 5.0
            });

            builder.HasData(new Bar()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                Address = "Medicinerhuset, Bygning 1161, Ole Worms Allé 4, 8000 Aarhus",
                AgeLimit = 18,
                Educations = "Medicin",
                ShortDescription = "Der er varme øl",
                LongDescription = "Der er alt for mange mennesker og alt for få øl",
                CVR = 29129932,
                PhoneNumber = 51927090,
                Email = "bestyrelsen@umbi.dk",
                AvgRating = 3.0
            });

            #endregion
        }
    }
}
