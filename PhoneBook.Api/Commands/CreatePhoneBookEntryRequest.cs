using MediatR;

namespace PhoneBook.Api.Commands;

public record CreatePhoneBookEntryRequest
(Guid PhoneBookId,
 string Name,
 string PhoneNumber) : IRequest<DTOs.PhoneBookEntry>;