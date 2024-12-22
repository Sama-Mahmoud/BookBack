using BookTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookTest
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options) { }
        public DbSet<Book> Book { get; set; }
    }
}
