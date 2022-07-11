using MediatR;

namespace PhoneBook.Core.Queries;

public record GetPhoneBooksRequest : IRequest<IReadOnlyCollection<DTOs.PhoneBook>>
{
    public string? SearchText { get; set; }
};