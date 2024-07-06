using BookManagement.Domain.Models;
using BookManagement.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Repository.Config
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name)
                .IsRequired();

            builder.Property(b => b.Author)
                .IsRequired();

            builder.Property(b => b.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(b => b.Stock)
                .IsRequired();

            builder.ToTable(table => table.HasCheckConstraint("CK_Book_Price_NonNegative", "[Price] >= 0"));
            builder.ToTable(table => table.HasCheckConstraint("CK_Book_Stock_NonNegative", "[Stock] >= 0"));
        }
    }
}
