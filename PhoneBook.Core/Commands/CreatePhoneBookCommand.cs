using MediatR;

namespace PhoneBook.Core.Commands;

public record CreatePhoneBookCommand(string Name): IRequest<DTOs.PhoneBook>;