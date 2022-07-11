using FluentAssertions;
using Microsoft.Extensions.Primitives;
using PhoneBook.Api.Validators;
using PhoneBook.DTOs;

namespace PhoneBook.Api.Tests;

public class CreatePhoneBookEntryRequestValidatorTests
{
    [Fact]
    public void ValidateTests_ShouldGiveValidationErrorForInvalidRequest()
    {
        var request = new AddPhoneBookEntryRequest
        {
            PhoneBookId = Guid.Empty,
            Name = string.Empty,
            PhoneNumbers = StringValues.Empty
        };

        var validator = new CreatePhoneBookEntryRequestValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(3);
    }
}