using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int8");

            builder.Property(prop => prop.Name)
                .HasMaxLength(150)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar(150)");
            
            builder.Property(prop => prop.Image)
                .IsRequired()
                .HasColumnName("Image")
                .HasColumnType("varchar");

            builder.Property(prop => prop.Description)
                .HasMaxLength(2000)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("varchar(2000)");

            builder.Property(prop => prop.Stock)
                .IsRequired()
                .HasColumnName("Stock")
                .HasColumnType("int8");
            
            builder.Property(prop => prop.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("bool")
                .HasDefaultValue(true);
            
            builder.Property(prop => prop.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("numeric(10, 2)");
        }
    }
}