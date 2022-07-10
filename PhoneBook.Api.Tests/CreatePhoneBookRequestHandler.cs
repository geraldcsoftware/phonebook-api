using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using PhoneBook.Api.CommandHandlers;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;

namespace PhoneBook.Api.Tests;

public class CreatePhoneBookRequestHandlerTests
{
    [Fact]
    public void HandleTests_ShouldCreatePhoneBookRecordInDatabase()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<PhoneBookDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var dbContext = new PhoneBookDbContext(dbContextOptions);

        var handler = new CreatePhoneBookRequestHandler(dbContext, new NullLogger<CreatePhoneBookRequestHandler>());
        var request = new CreatePhoneBookRequest("Test Phone Book");

        // Act
        var result = handler.Handle(request, default).GetAwaiter().GetResult();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Phone Book");
        result.NumberOfEntries.Should().Be(0);
    }
}