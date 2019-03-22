using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class BarrepresentativeConfiguration : IEntityTypeConfiguration<Barrepresentative>
    {
        public void Configure(EntityTypeBuilder<Barrepresentative> builder)
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
