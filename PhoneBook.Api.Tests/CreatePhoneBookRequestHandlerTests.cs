using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using PhoneBook.Core.CommandHandlers;
using PhoneBook.Core.Commands;
using PhoneBook.Data;

namespace PhoneBook.Api.Tests;

public class CreatePhoneBookRequestHandlerTests
{
    [Fact]
    public async Task HandleTests_ShouldCreatePhoneBookRecordInDatabase()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<PhoneBookDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var dbContext = new PhoneBookDbContext(dbContextOptions);

        var handler = new CreatePhoneBookRequestHandler(dbContext, new NullLogger<CreatePhoneBookRequestHandler>());
        var request = new CreatePhoneBookCommand("Test Phone Book");

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Phone Book");
        result.NumberOfEntries.Should().Be(0);

       var persistedPhoneBook = await dbContext.PhoneBooks.FirstOrDefaultAsync(p => p.Id == result.Id);
       persistedPhoneBook.Should().NotBeNull();
    }
}