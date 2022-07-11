using FastEndpoints;
using MediatR;
using PhoneBook.Core.Queries;

namespace PhoneBook.Api.Endpoints;

public class GetPhoneBooksEndpoint: EndpointWithoutRequest<List<DTOs.PhoneBook>>
{
    private readonly IMediator _mediator;

    public GetPhoneBooksEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/phonebooks");
    }

    public override async Task<List<DTOs.PhoneBook>> ExecuteAsync(CancellationToken ct)
    {
        var searchText = Query<string?>("searchText");
        var request = new GetPhoneBooksRequest { SearchText = searchText };
        var phoneBooks = await _mediator.Send(request, ct);
        return phoneBooks.ToList();
    }
}