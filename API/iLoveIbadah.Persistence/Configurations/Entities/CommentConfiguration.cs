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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasTrigger("Trigger_Update_Comment");
            });
            builder.ToTable("Comment");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.BlogId).HasColumnName("Blog_id");
            builder.Property(e => e.UserAccountId).HasColumnName("User_Account_id");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.WrittenAt).HasColumnName("written_at");
            builder.Property(e => e.LastUpdatedAt).HasColumnName("last_updated_at");
            builder.Property(e => e.ParentCommentId).HasColumnName("Comment_id");
        }
    }
}
