using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Core.Commands;
using PhoneBook.Data;

namespace PhoneBook.Core.CommandHandlers;

public class CreatePhoneBookRequestHandler : IRequestHandler<CreatePhoneBookCommand, DTOs.PhoneBook>
{
    private readonly PhoneBookDbContext _dbContext;
    private readonly ILogger<CreatePhoneBookRequestHandler> _logger;

    public CreatePhoneBookRequestHandler(PhoneBookDbContext dbContext,
                                         ILogger<CreatePhoneBookRequestHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<DTOs.PhoneBook> Handle(CreatePhoneBookCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var phoneBookEntity = new Data.Models.PhoneBook
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };
        _logger.LogInformation("Adding phonebook record {@PhoneBook}", phoneBookEntity);

        _dbContext.PhoneBooks.Add(phoneBookEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new()
        {
            Id = phoneBookEntity.Id,
            Name = phoneBookEntity.Name
        };
    }
}