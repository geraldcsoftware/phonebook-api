namespace PhoneBook.Api.DTOs;

public class PhoneBook
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int NumberOfEntries { get; set; }
}