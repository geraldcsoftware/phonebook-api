using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Api.Data;

public class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {
    }

    public DbSet<Models.PhoneBook> PhoneBooks => Set<Models.PhoneBook>();
    public DbSet<Models.PhoneBookEntry> Entries => Set<Models.PhoneBookEntry>();
}