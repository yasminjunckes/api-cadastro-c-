using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class UserMapping: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User(
                Guid.Parse("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce"),
                "User Test",
                "24068108013",
                "01/01/2000",
                "usertest@gmail.com",
                "47992345671"
            ));

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.PersonalDocument)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(x => x.BirthDate)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Phone)
                .HasMaxLength(11);

            builder
                .Property(x => x.RemovedAt);
        }
    }
}
