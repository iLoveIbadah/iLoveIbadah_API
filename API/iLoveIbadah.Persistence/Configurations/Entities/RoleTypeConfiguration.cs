using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iLoveIbadah.Domain;

namespace iLoveIbadah.Persistence.Configurations.Entities
{
    public class RoleTypeConfiguration : IEntityTypeConfiguration<RoleType>
    {
        public void Configure(EntityTypeBuilder<RoleType> builder)
        {
            builder.ToTable("Role_Type");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.FullName).HasColumnName("full_name");
            builder.Property(e => e.Details).HasColumnName("details");
        }
    }
}
