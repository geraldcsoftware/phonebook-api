using MediatR;

namespace PhoneBook.Core.Queries;

public record GetPhoneEntriesRequest(Guid PhoneBookId, string SearchText): IRequest<IReadOnlyCollection<DTOs.PhoneBookEntry>>;