using FluentValidation;
using PhoneBook.Api.DTOs;

namespace PhoneBook.Api.Validators;

public class CreatePhoneBookRequestValidator: AbstractValidator<CreatePhoneBookRequest>
{
    public CreatePhoneBookRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}