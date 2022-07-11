using FluentAssertions;
using PhoneBook.Api.Validators;
using PhoneBook.DTOs;

namespace PhoneBook.Api.Tests;

public class CreatePhoneBookRequestValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData((string)null!)]
    public void ValidateTests_ShouldGiveErrorForInvalidName(string name)
    {
        var validator = new CreatePhoneBookRequestValidator();

        var result = validator.Validate(new CreatePhoneBookRequest { Name = name });

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().PropertyName.Should().Be(nameof(CreatePhoneBookRequest.Name));
    }

    [Fact]
    public void ValidateTests_ShouldNotHaveErrorForValidName()
    {
        var validator = new CreatePhoneBookRequestValidator();

        var result = validator.Validate(new CreatePhoneBookRequest { Name = "Test" });

        result.IsValid.Should().BeTrue();
    }
}