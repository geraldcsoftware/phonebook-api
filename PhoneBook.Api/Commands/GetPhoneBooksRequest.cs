using MediatR;

namespace PhoneBook.Api.Commands;

public record GetPhoneBooksRequest : IRequest<IReadOnlyCollection<DTOs.PhoneBook>>
{
    public string? SearchText { get; set; }
};