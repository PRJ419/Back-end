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
                AvgRating = 5.0,
                Image = "https://scontent-dus1-1.xx.fbcdn.net/v/t1.0-9/13166_441233562611984_1450333570_n.png?_nc_cat=105&_nc_ht=scontent-dus1-1.xx&oh=3a0e9139a633dd8d9131afd229eab1da&oe=5D2B6EDD"
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
                AvgRating = 3.0,
                Image = "https://scontent-dus1-1.xx.fbcdn.net/v/t1.0-9/43698279_2427965823910886_4605085834809442304_n.png?_nc_cat=111&_nc_ht=scontent-dus1-1.xx&oh=e3bc006d52005d545011cc52b1f8a7d8&oe=5D76ACBE"
            });

            builder.HasData(new Bar()
            {
                BarName = "Tågekammeret",
                Address = "Ny Munkegade 118, 8000 Aarhus",
                AgeLimit = 18,
                Educations = "Fysik, Datalogi, IT Bachelor, Matematik-Økonomi, Nanoteknologi",
                ShortDescription = "Husk Tågelygter!",
                LongDescription = "Du ender i et sort hul, hvis du bliver ved med at drikke her",
                CVR = 34126399,
                PhoneNumber = 87154052,
                Email = "BEST@TAAGEKAMMERET.dk",
                AvgRating = 4.0,
                Image = "https://taagekammeret.dk/static/TKlogo.jpg",
  
            });

            #endregion
        }
    }
}
