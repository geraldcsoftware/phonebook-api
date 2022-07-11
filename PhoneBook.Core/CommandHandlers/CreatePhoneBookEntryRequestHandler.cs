using System.Text.Json;
using MediatR;
using PhoneBook.Core.Commands;
using PhoneBook.Data;
using PhoneBook.DTOs;

namespace PhoneBook.Core.CommandHandlers;

public class CreatePhoneBookEntryRequestHandler : IRequestHandler<CreatePhoneBookEntryCommand, PhoneBookEntry>
{
    private readonly PhoneBookDbContext _dbContext;

    public CreatePhoneBookEntryRequestHandler(PhoneBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PhoneBookEntry> Handle(CreatePhoneBookEntryCommand command,
                                             CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var phoneBookEntry = new Data.Models.PhoneBookEntry
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            PhoneBookId = command.PhoneBookId,
            PhoneNumber = JsonSerializer.Serialize(command.PhoneNumbers)
        };

        _dbContext.Entries.Add(phoneBookEntry);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PhoneBookEntry
        {
            Id = phoneBookEntry.Id,
            Name = phoneBookEntry.Name,
            PhoneNumbers = JsonSerializer.Deserialize<string[]>(phoneBookEntry.PhoneNumber)!,
            PhoneBookId = phoneBookEntry.PhoneBookId
        };
    }
}