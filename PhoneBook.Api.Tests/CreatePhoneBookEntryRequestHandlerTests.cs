using System.Text.Json;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using PhoneBook.Api.CommandHandlers;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using PhoneBook.Api.Mapping;
using Models = PhoneBook.Api.Data.Models;

namespace PhoneBook.Api.Tests;

public class CreatePhoneBookEntryRequestHandlerTests
{
    [Fact]
    public void HandleTests_ShouldAddPhoneBookEntryToDatabase()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<PhoneBookDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var dbContext = new PhoneBookDbContext(dbContextOptions);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMapper();
        var mapper = serviceCollection.BuildServiceProvider().GetRequiredService<IMapper>();
        
        var handler = new CreatePhoneBookEntryRequestHandler(dbContext, mapper);

        var phoneBook = new Models.PhoneBook
        {
            Id = Guid.NewGuid(),
            Name = "Test Phone Book",
        };
        dbContext.PhoneBooks.Add(phoneBook);
        dbContext.SaveChanges();
        
        var request = new CreatePhoneBookEntryRequest(phoneBook.Id, "Test Entry", new StringValues("0012344590459"));

        // Act
        var result = handler.Handle(request, default).GetAwaiter().GetResult();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Entry");
    }
}