using Microsoft.Extensions.Primitives;

namespace PhoneBook.Api.DTOs;

public class PhoneBookEntry
{
    public Guid Id { get; set; }
    public Guid PhoneBookId { get; set; }
    public string? Name { get; set; }
    public ICollection<string> PhoneNumbers { get; set; } = StringValues.Empty;
}