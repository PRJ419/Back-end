using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
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
                BarName = "Katrines Kælder",
                EventName = "Bingo Bar",
                Date = new DateTime(2019,02,25),
                Image = "https://letsbingo.dk/wp-content/uploads/2016/01/Bingo-graphic21.jpg"
            });

            builder.HasData(new BarEvent()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                EventName = "Andreas på tur!",
                Date = new DateTime(2019,05,29),
                Image = "https://media1.s-nbcnews.com/i/newscms/2016_48/1811466/161128-drinking-alcohol-jpo-108p_52ad934c90bc61c93c2242c4349f5e55.jpg"
            });

            builder.HasData(new BarEvent()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                EventName = "Ipod Battle Bar",
                Date = new DateTime(2019,03,05),
                Image = "https://ecdn.evensi.com/e301908712?cph=gAAAAABcvXMS1wYy7ZyTjIpjScMOwT9aERAm7bQrt8iS-5I7umkCbr2B_3zz_OTxoGxamKgI6Su1nCvudV9KA74kF3CljhF-AJEPUhQt7K-NfWz1IbgakBxjcqUGS98Pw-gQ8RLV39CMKlB13oChr1i6Ai8xjvDMOfQ-aJr-3abxG37hRxzubXfSky6WWGnYlQDbioz7SjJXMWE3eT1mPcBhMZox_B0gQLJw7cR802diHabhG3iV17hgvr0-zOUE-DlWTjFEuK_TuZ1yFd6kuC3BWwBswtOM58r_prd6HgX2ETPXJ9iFge65DKMKUsXhVCrwB5GjRBXU0NXP9MccSFwpN2rgrXAXykspY9N_FFUMbaJOZGlhllsH89N4QKdv4oba32VDCPa9"
            });

            builder.HasData(new BarEvent()
            {
                BarName = "Tågekammeret",
                EventName = "Bodycrashing",
                Date = new DateTime(2019,06,20),
                Image = "https://taagekammeret.dk/media/__sized__/2016/bodycrashing/15271383_10210607041708452_1374044913_o-crop-c0-5__0-5-253x253-70.jpg"
            });

            #endregion
        }
    }
}
