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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Title).HasColumnName("title");
            builder.Property(e => e.Slug).HasColumnName("slug");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.TotalViews).HasColumnName("total_views")
                .HasDefaultValue();
            builder.Property(e => e.CreatedAt).HasColumnName("created_at")
                .ValueGeneratedOnAddOrUpdate();
            builder.Property(e => e.BlobFileId).HasColumnName("Blob_File_id");
        }
    }
}
