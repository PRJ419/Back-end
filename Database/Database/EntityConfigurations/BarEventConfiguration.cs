using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class BarEventConfiguration : IEntityTypeConfiguration<BarEvent>
    {
        public void Configure(EntityTypeBuilder<BarEvent> builder)
        {
            builder
                .HasKey(key => new { key.BarName, key.EventName });

            builder
                .HasOne(b => b.Bar)
                .WithMany(e => e.BarEvents)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(b => b.BarName);

            #region Dataseeding

            builder.HasData(new BarEvent()
            {
                BarName = "Katrines Kælder",
                EventName = "Tobias tager Level Up",
                Date = new DateTime(2019,04,26)
            });

            builder.HasData(new BarEvent()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                EventName = "Andreas på tur!",
                Date = new DateTime(2019,05,29)                
            });

            #endregion
        }
    }
}
