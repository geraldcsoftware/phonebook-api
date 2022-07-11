using MediatR;
using PhoneBook.DTOs;

namespace PhoneBook.Core.Commands;

public record CreatePhoneBookEntryCommand
(Guid PhoneBookId,
 string Name,
 IReadOnlyCollection<string> PhoneNumbers) : IRequest<PhoneBookEntry>;