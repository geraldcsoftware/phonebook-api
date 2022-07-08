using MediatR;

namespace PhoneBook.Api.Commands;

public record CreatePhoneBookEntryRequest
(Guid PhoneBookId,
 string Name,
 IReadOnlyCollection<string> PhoneNumbers) : IRequest<DTOs.PhoneBookEntry>;