using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(a => a.Username);

            builder
                .HasIndex(a => a.Email)
                .IsUnique();

            #region Dataseeding

            builder.HasData(new Customer()
            {
                Username = "Bodega Bent",
                Name = "Bent",
                DateOfBirth = new DateTime(1990,01,01),
                Email = "JegElskerØl@Yahoo.com",
                FavoriteBar = "Katrines Kælder",
                FavoriteDrink = "Fadøl"
            });

            builder.HasData(new Customer()
            {
                Username = "Dehydrerede Dennis",
                Name = "Dennis",
                DateOfBirth = new DateTime(1990,02,02),
                Email = "JegErTørstig@gmail.com",
                FavoriteBar = "Medicinsk Fredagsbar - Umbilicus",
                FavoriteDrink = "Vodka Redbull"
            });


            builder.HasData(new Customer()
            {
                Username = "Koffein Karsten",
                Name = "Karsten",
                DateOfBirth = new DateTime(1990,03,03),
                Email = "KaffeTrolden@gmail.com",
                FavoriteBar = "Tågekammeret",
                FavoriteDrink = "Kaffe",
            });

            #endregion
        }
    }
}
