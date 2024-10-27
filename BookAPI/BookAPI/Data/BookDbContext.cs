using BookAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data;
public class BookDbContext : DbContext {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }



}
