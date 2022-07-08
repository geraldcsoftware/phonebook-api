using System.Text.Json;
using MapsterMapper;
using MediatR;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using PhoneBook.Api.DTOs;

namespace PhoneBook.Api.CommandHandlers;

public class CreatePhoneBookEntryRequestHandler : IRequestHandler<CreatePhoneBookEntryRequest, PhoneBookEntry>
{
    private readonly PhoneBookDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreatePhoneBookEntryRequestHandler(PhoneBookDbContext dbContext,
                                              IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PhoneBookEntry> Handle(CreatePhoneBookEntryRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var phoneBookEntry = new Data.Models.PhoneBookEntry
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneBookId = request.PhoneBookId,
            PhoneNumber = JsonSerializer.Serialize(request.PhoneNumbers)
        };

        _dbContext.Entries.Add(phoneBookEntry);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PhoneBookEntry>(phoneBookEntry);
    }
}