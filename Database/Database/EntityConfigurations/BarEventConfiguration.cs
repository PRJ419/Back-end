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
        }
    }
}
