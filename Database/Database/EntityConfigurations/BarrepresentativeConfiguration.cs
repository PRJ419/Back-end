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
                .WithMany(b => b.Barrepresentatives)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.BarName);

            builder
                .HasKey(a => a.Username);
        }
    }
}
