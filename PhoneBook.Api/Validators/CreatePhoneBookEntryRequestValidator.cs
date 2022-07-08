using System.Text.RegularExpressions;
using FluentValidation;

namespace PhoneBook.Api.Validators;

public class CreatePhoneBookEntryRequestValidator : AbstractValidator<DTOs.AddEntryRequest>
{
    public CreatePhoneBookEntryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.PhoneBookId).NotNull().NotEmpty();
        RuleFor(x => x.PhoneNumbers).NotNull().NotEmpty()
                                    .Must(x =>
                                     {
                                         return x is { Count: >= 1 } &&
                                                x.All(val => Regex.IsMatch(val, @"^(\+|00)[1-9][0-9]{10,13}"));
                                     });
    }
}