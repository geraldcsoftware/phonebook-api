using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data.Configuration;

namespace PhoneBook.Api.Data.Models;

[EntityTypeConfiguration(typeof(PhoneBookEntryEntityConfiguration))]
public class PhoneBookEntry
{
    public Guid Id { get; set; }
    public Guid PhoneBookId { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
}