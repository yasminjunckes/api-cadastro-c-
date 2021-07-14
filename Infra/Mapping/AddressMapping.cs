using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class AddressMapping: IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(new Address(
                "line1",
                "line2",
                100,
                "89055050",
                "City",
                "SC",
                "District",
                true,
                Guid.Parse("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce")
                ));

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
