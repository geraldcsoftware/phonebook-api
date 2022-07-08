using MapsterMapper;
using MediatR;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;

namespace PhoneBook.Api.CommandHandlers;

public class CreatePhoneBookRequestHandler : IRequestHandler<CreatePhoneBookRequest, DTOs.PhoneBook>
{
    private readonly PhoneBookDbContext _dbContext;
    private readonly ILogger<CreatePhoneBookRequestHandler> _logger;

    public CreatePhoneBookRequestHandler(PhoneBookDbContext dbContext,
                                         ILogger<CreatePhoneBookRequestHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<DTOs.PhoneBook> Handle(CreatePhoneBookRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var phoneBookEntity = new Data.Models.PhoneBook
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        _logger.LogInformation("Adding phonebook record {@PhoneBook}", phoneBookEntity);

        _dbContext.PhoneBooks.Add(phoneBookEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new DTOs.PhoneBook
        {
            Id = phoneBookEntity.Id,
            Name = phoneBookEntity.Name,
            NumberOfEntries = 0
        };
    }
}