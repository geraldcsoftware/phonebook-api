using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using PhoneBook.Core.CommandHandlers;
using PhoneBook.Core.Commands;
using PhoneBook.Data;

namespace PhoneBook.Api.Tests;

public class CreatePhoneBookEntryRequestHandlerTests
{
    [Fact]
    public async Task HandleTests_ShouldAddPhoneBookEntryToDatabase()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<PhoneBookDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var dbContext = new PhoneBookDbContext(dbContextOptions);

        var handler = new CreatePhoneBookEntryRequestHandler(dbContext);

        var phoneBook = new Data.Models.PhoneBook
        {
            Id = Guid.NewGuid(),
            Name = "Test Phone Book",
        };
        dbContext.PhoneBooks.Add(phoneBook);
        await dbContext.SaveChangesAsync();

        var request = new CreatePhoneBookEntryCommand(phoneBook.Id, "Test Entry", new StringValues("0012344590459"));

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Entry");

        var persistedEntry = await dbContext.Entries.FirstOrDefaultAsync(x => x.PhoneBookId == phoneBook.Id &&
                                                                              x.PhoneNumber == "0012344590459");
        persistedEntry.Should().NotBeNull();
    }
}