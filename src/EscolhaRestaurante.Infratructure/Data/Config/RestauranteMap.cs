using EscolhaRestaurante.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.Infratructure.Data.Config
{
    public class RestauranteMap : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {
            builder.HasKey(r => r.RestauranteID);

            builder.HasMany(r => r.Votos)
                .WithOne(v => v.Restaurante)
                .HasForeignKey(r => r.RestauranteID)
                .HasPrincipalKey(v => v.RestauranteID);

            builder.Property(r => r.Nome)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(r => r.UltimaEscolha)
                .HasColumnType("DateTime");
        }
    }
}
