using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.Mapping
{
    public class AdressMapping: IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder
                .Property(x => x.Line1)
                .HasMaxLength(100);

            builder
                .Property(x => x.Line2)
                .HasMaxLength(100);

            builder
                .Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .Property(x => x.PostalCode)
                .IsRequired()
                .HasMaxLength(9);

            builder
                .Property(x => x.City);

            builder
                .Property(x => x.State)
                .HasMaxLength(2);

            builder
                .Property(x => x.District);

            builder
                .Property(x => x.Principal)
                .IsRequired();

            builder
                .Property(x => x.UserId)
                .IsRequired();
            
            builder
                .HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }

    }
}
