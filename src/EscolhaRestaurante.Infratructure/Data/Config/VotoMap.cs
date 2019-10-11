using EscolhaRestaurante.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.Infratructure.Data.Config
{
    public class VotoMap : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {
            builder.HasKey(v => v.VotoID);

            builder.HasOne(v => v.Restaurante)
                .WithMany(r => r.Votos)
                .HasForeignKey(v => v.RestauranteID);

            builder.Property(v => v.Nome)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(v => v.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(v => v.DataVoto)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(v => v.RestauranteID)
                .IsRequired();
        }
    }
}
