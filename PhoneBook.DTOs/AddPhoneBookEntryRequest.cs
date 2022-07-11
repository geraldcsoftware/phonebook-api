namespace PhoneBook.DTOs;

public class AddPhoneBookEntryRequest
{
    public Guid? PhoneBookId { get; set; }
    public string? Name { get; set; }
    public IReadOnlyCollection<string>? PhoneNumbers { get; set; }
}