using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class BarRepresentativeConfiguration : IEntityTypeConfiguration<BarRepresentative>
    {
        public void Configure(EntityTypeBuilder<BarRepresentative> builder)
        {
            builder
                .HasOne(a => a.Bar)
                .WithMany(b => b.BarRepresentatives)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.BarName);

            builder
                .HasKey(a => a.Username);

            #region Dataseeding

            builder.HasData(new BarRepresentative()
            {
                Username = "Legend27",
                Name = "Ole Ølmave",
                BarName = "Katrines Kælder"
            });

            builder.HasData(new BarRepresentative()
            {
                Username = "Kratluskeren",
                Name = "Tørstige Torsten",
                BarName = "Medicinsk Fredagsbar - Umbilicus"
            });

            #endregion

        }
    }
}
