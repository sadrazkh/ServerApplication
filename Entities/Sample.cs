using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Sample : BaseEntity<Guid>
    {
        public string FirstTestField { get; set; }
        public string SecondTestField { get; set; }

    }

    public class SampleConfiguration : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder.Property(p => p.FirstTestField).IsRequired().HasMaxLength(100);
        }
    }
}
