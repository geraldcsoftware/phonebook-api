using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Configuration;

namespace PhoneBook.Data.Models;

[EntityTypeConfiguration(typeof(PhoneBookEntityConfiguration))]
public class PhoneBook
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<PhoneBookEntry> Entries { get; } = new List<PhoneBookEntry>();
}