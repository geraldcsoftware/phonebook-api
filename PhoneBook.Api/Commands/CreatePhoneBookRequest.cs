using MediatR;

namespace PhoneBook.Api.Commands;

public record CreatePhoneBookRequest(string Name): IRequest<DTOs.PhoneBook>;