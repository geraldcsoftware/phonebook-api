namespace PhoneBook.Api.DTOs;

public class AddEntryRequest
{
    public Guid? PhoneBookId { get; set; }
    public string? Name { get; set; }
    public IReadOnlyCollection<string>? PhoneNumbers { get; set; }
}