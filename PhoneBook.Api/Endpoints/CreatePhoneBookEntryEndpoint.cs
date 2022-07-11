using FastEndpoints;
using MediatR;
using PhoneBook.Core.Commands;
using PhoneBook.DTOs;

namespace PhoneBook.Api.Endpoints;

public class CreatePhoneBookEntryEndpoint : Endpoint<AddPhoneBookEntryRequest, PhoneBookEntry>
{
    private readonly IMediator _mediator;

    public CreatePhoneBookEntryEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/phonebooks/{phoneBookId:guid}/entries");
    }

    public override async Task<PhoneBookEntry> ExecuteAsync(AddPhoneBookEntryRequest req, CancellationToken ct)
    {
        var phoneBookId = Route<Guid>("phoneBookId");
        var command = new CreatePhoneBookEntryCommand(phoneBookId, req.Name!, req.PhoneNumbers!);
        var entry = await _mediator.Send(command, ct);

        return entry;
    }
}