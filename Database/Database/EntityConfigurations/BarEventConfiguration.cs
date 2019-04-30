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
                Date = new DateTime(2019,04,26),
                Image = "https://vignette.wikia.nocookie.net/my-hero-academia-fanon/images/0/0a/Level_Up.png/revision/latest?cb=20180722000746"
            });

            builder.HasData(new BarEvent()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                EventName = "Andreas på tur!",
                Date = new DateTime(2019,05,29),
                Image = "https://media1.s-nbcnews.com/i/newscms/2016_48/1811466/161128-drinking-alcohol-jpo-108p_52ad934c90bc61c93c2242c4349f5e55.jpg"
            });

            #endregion
        }
    }
}
