using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos.Mapping
{
    public class PasswordMapping : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {

            builder.ToTable("Password").HasKey(p => p.IdPassword);

            builder.HasOne(e => e.Email)
                .WithMany(p => p.Passwords)
                .HasForeignKey(d => d.IdEmail);
            builder.Property(p => p.PasswordEncriptada);

        }
    }
}
