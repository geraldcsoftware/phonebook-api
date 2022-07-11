using FastEndpoints;
using MediatR;
using PhoneBook.Core.Commands;

namespace PhoneBook.Api.Endpoints;

public class CreatePhoneBookEndpoint : Endpoint<DTOs.CreatePhoneBookRequest, DTOs.PhoneBook>
{
    private readonly IMediator _mediator;

    public CreatePhoneBookEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/phonebooks");
    }

    public override async Task<DTOs.PhoneBook> ExecuteAsync(DTOs.CreatePhoneBookRequest request, CancellationToken ct)
    {
        var command = new CreatePhoneBookCommand(request.Name!);
        var phoneBook = await _mediator.Send(command, ct);
        return phoneBook;
    }
}