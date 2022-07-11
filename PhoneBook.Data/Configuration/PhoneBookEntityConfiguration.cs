using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoneBook.Data.Configuration;

public class PhoneBookEntityConfiguration : IEntityTypeConfiguration<Models.PhoneBook>
{
    public void Configure(EntityTypeBuilder<Models.PhoneBook> builder)
    {
        builder.ToTable("PhoneBooks");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(200);
    }
}