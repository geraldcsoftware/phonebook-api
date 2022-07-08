using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Api.Data.Models;

namespace PhoneBook.Api.Data.Configuration;

public class PhoneBookEntryEntityConfiguration : IEntityTypeConfiguration<PhoneBookEntry>
{
    public void Configure(EntityTypeBuilder<PhoneBookEntry> builder)
    {
        builder.ToTable("PhoneBookEntries");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(200);
        builder.Property(x => x.PhoneNumber).IsRequired().IsUnicode(false).HasMaxLength(300);

        builder.HasOne<Models.PhoneBook>()
               .WithMany(x => x.Entries)
               .HasForeignKey(x => x.PhoneBookId);
    }
}