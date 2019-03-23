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

        }
    }
}
