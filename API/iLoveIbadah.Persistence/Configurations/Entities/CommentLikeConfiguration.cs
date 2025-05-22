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
    public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.ToTable("Comment_Like");
            builder.HasKey(cl => new { cl.CommentId, cl.UserAccountId });
            builder.Property(e => e.CommentId).HasColumnName("Comment_id");
            builder.Property(e => e.UserAccountId).HasColumnName("User_Account_Id");
        }
    }
}
