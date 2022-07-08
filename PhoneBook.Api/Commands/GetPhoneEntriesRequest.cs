using MediatR;

namespace PhoneBook.Api.Commands;

public record GetPhoneEntriesRequest(Guid PhoneBookId, string SearchText): IRequest<IReadOnlyCollection<DTOs.PhoneBookEntry>>;