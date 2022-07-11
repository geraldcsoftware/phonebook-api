using FastEndpoints;
using MediatR;
using PhoneBook.Core.Queries;
using PhoneBook.DTOs;

namespace PhoneBook.Api.Endpoints;

public class GetPhoneBookEntriesEndpoint : EndpointWithoutRequest<List<PhoneBookEntry>>
{
    private readonly IMediator _mediator;

    public GetPhoneBookEntriesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/phonebooks/{phoneBookId:guid}/entries");
    }

    public override async Task<List<PhoneBookEntry>> ExecuteAsync(CancellationToken ct)
    {
        var phoneBookId = Route<Guid>("phoneBookId");
        var searchText = Query<string?>("searchText");
        var request = new GetPhoneEntriesRequest(phoneBookId, searchText!);
        var phoneBookEntries = await _mediator.Send(request, ct);
        return phoneBookEntries.ToList();
    }
}