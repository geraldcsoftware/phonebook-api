using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Api.Data;

public class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {
    }
}