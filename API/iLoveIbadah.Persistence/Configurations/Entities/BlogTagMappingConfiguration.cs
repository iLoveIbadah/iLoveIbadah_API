using iLoveIbadah.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Persistence.Configurations.Entities
{
    public class BlogTagMappingConfiguration : IEntityTypeConfiguration<BlogTagMapping>
    {
        public void Configure(EntityTypeBuilder<BlogTagMapping> builder)
        {
            builder.ToTable("Blog_Tag_Mapping");
            builder.Property(e => e.BlogId).HasColumnName("Blog_id");
            builder.Property(e => e.TagId).HasColumnName("Tag_id");
        }
    }
}
