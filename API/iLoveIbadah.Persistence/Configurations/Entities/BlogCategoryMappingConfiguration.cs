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
    public class BlogCategoryMappingConfiguration : IEntityTypeConfiguration<BlogCategoryMapping>
    {
        public void Configure(EntityTypeBuilder<BlogCategoryMapping> builder)
        {
            builder.ToTable("Blog_Category_Mapping");
            builder.HasKey(bc => new { bc.BlogId, bc.CategoryId }); // entity framework requit chaque entity to have a key, or i need to do HasNoKey but then it is goof only for when it is readonly table, but i need crud operations then putting has composite key is the best!
            builder.Property(e => e.BlogId).HasColumnName("Blog_id");
            builder.Property(e => e.CategoryId).HasColumnName("Category_id");
        }
    }
}
