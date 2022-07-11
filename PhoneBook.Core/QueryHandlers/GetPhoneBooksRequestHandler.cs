using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Queries;
using PhoneBook.Data;

namespace PhoneBook.Core.QueryHandlers;

public class GetPhoneBooksRequestHandler : IRequestHandler<GetPhoneBooksRequest, IReadOnlyCollection<DTOs.PhoneBook>>
{
    private readonly PhoneBookDbContext _dbContext;

    public GetPhoneBooksRequestHandler(PhoneBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DTOs.PhoneBook>> Handle(GetPhoneBooksRequest request,
                                                                  CancellationToken cancellationToken)
    {
        var phoneBooksRequest = _dbContext.PhoneBooks.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var matchText = $"%{request.SearchText.Trim()}%";
            phoneBooksRequest = phoneBooksRequest.Where(p => EF.Functions.Like(p.Name!, matchText));
        }

        var phoneBooks = await phoneBooksRequest.Select(p => new DTOs.PhoneBook
        {
            Name = p.Name,
            Id = p.Id,
            NumberOfEntries = p.Entries.Count
        }).ToListAsync(cancellationToken);

        return phoneBooks;
    }
}