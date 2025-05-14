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
    public class BlogLikeConfiguration : IEntityTypeConfiguration<BlogLike>
    {
        public void Configure(EntityTypeBuilder<BlogLike> builder)
        {
            builder.ToTable("Blog_Like");
            builder.Property(e => e.BlogId).HasColumnName("Blog_id");
            builder.Property(e => e.UserAccountId).HasColumnName("User_Account_Id");
        }
    }
}
