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
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(4000);
            builder.Property(a => a.CoverImageUrl)
                .HasMaxLength(1000);
            builder.Property(a => a.CategoryId)
                .IsRequired();
        }
    }
}
