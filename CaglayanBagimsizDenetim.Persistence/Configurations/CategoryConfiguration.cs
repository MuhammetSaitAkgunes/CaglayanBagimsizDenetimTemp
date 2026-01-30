using CaglayanBagimsizDenetim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Tablo adı (Opsiyonel ama iyi bir pratik)
            builder.ToTable("Categories");
            // Primary Key
            builder.HasKey(c => c.Id);
            // Property Ayarları
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100); // Veritabanında NVARCHAR(100) olacak
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
