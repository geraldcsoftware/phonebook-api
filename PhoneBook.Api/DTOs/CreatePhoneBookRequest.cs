using MediatR;

namespace PhoneBook.Api.DTOs;

public class CreatePhoneBookRequest : IRequest<Unit>
{
    public string? Name { get; set; }
}