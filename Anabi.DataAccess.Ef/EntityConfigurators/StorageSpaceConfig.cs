﻿using Microsoft.EntityFrameworkCore;
using Anabi.DataAccess.Ef.DbModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace Anabi.DataAccess.Ef.EntityConfigurators
{
    public class StorageSpaceConfig : IEntityConfig
    {
        public void SetupEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<StorageSpaceDb>();
            entity.ToTable("StorageSpaces");

            entity.HasKey(k => k.Id);
            entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
            entity.Property(p => p.StorageSpacesType)
                .HasConversion(c => c.ToString(),
                    c => (StorageSpaceDb.StorageSpaceType)Enum.Parse(typeof(StorageSpaceDb.StorageSpaceType), c));

            entity.HasIndex(i => i.Name).IsUnique();

            entity.HasOne(a => a.Address)
                .WithOne(s => s.StorageRoom)
                .HasForeignKey<StorageSpaceDb>(k => k.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_StorageSpaces_Addresses")
                .IsRequired();

            entity.HasOne(c => c.Category)
                .WithMany(s => s.StorageSpaces)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_StorageSpaces_Categories")
                .IsRequired();

            entity.Property(p => p.TotalVolume)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.AvailableVolume)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.Length)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.Width)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.Description)
                .HasMaxLength(2000);

            entity.Property(p => p.AsphaltedArea)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.UndevelopedArea)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.ContactData)
                .HasMaxLength(1000);

            entity.Property(p => p.MonthlyMaintenanceCost)
                .HasColumnType("Decimal(20, 2)");

            entity.Property(p => p.MaintenanceMentions);
        }   
    }
}
