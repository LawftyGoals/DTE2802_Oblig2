using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data;
public class BookDbContext : DbContext {
    public DbSet<Book> Books { get; set; }



}
