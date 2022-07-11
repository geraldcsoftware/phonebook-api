using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Queries;
using PhoneBook.Data;
using PhoneBook.DTOs;

namespace PhoneBook.Core.QueryHandlers;

public class GetPhoneEntriesRequestHandler :
    IRequestHandler<GetPhoneEntriesRequest, IReadOnlyCollection<PhoneBookEntry>>
{
    private readonly PhoneBookDbContext _dbContext;

    public GetPhoneEntriesRequestHandler(PhoneBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<PhoneBookEntry>> Handle(GetPhoneEntriesRequest request,
                                                                  CancellationToken cancellationToken)
    {
        var query = _dbContext.Entries
                              .Where(x => x.PhoneBookId == request.PhoneBookId)
                              .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var matchText = $"%{request.SearchText.Trim()}%";
            query = query.Where(p => EF.Functions.Like(p.Name!, matchText) ||
                                     EF.Functions.Like(p.PhoneNumber!, matchText));
        }

        var phoneBookEntries = await query.ToListAsync(cancellationToken);
        return phoneBookEntries.Select(p => new PhoneBookEntry
        {
            Id = p.Id,
            Name = p.Name,
            PhoneBookId = p.PhoneBookId,
            PhoneNumbers = JsonSerializer.Deserialize<string[]>(p.PhoneNumber!)!
        }).ToList();
    }
}