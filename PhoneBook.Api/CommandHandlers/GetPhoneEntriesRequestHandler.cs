using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using PhoneBook.Api.DTOs;

namespace PhoneBook.Api.CommandHandlers;

public class GetPhoneEntriesRequestHandler :
    IRequestHandler<GetPhoneEntriesRequest, IReadOnlyCollection<PhoneBookEntry>>
{
    private readonly PhoneBookDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPhoneEntriesRequestHandler(PhoneBookDbContext dbContext,
                                         IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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
                                     EF.Functions.JsonContains(p.PhoneNumber!, request.SearchText));
        }

        var phoneBookEntries = await query.ToListAsync(cancellationToken);
        return _mapper.Map<IReadOnlyCollection<PhoneBookEntry>>(phoneBookEntries);
    }
}