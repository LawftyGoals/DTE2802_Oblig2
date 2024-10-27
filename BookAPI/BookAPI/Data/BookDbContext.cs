using BookAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data;
public class BookDbContext : DbContext {

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<Book>().ToTable("Book");

        builder.Entity<Book>().HasData(new Book() {
            BookId = 1,
            Title = "A Great Book",
            Description = "One for the ages.",
            Year = 2024,
            AuthorId = 1
        });
        builder.Entity<Book>().HasData(new Book() {
            BookId = 2,
            Title = "A Great Book, part 2",
            Description = "This one is not so great.",
            Year = 2024,
            AuthorId = 1
        });
        builder.Entity<Book>().HasData(new Book() {
            BookId = 3,
            Title = "A Silly Book",
            Description = "A silly book indeed.",
            Year = 2024,
            AuthorId = 2
        });
        builder.Entity<Book>().HasData(new Book() {
            BookId = 4,
            Title = "Alpha et Omega al.",
            Description = "So dense its stupidly briliant.",
            Year = 2024,
            AuthorId = 3
        });



        builder.Entity<Author>().HasData(new Author() {
            AuthorId = 1,
            FirstName = "Jack",
            LastName = "Author"
        });
        builder.Entity<Author>().HasData(new Author() {
            AuthorId = 2,
            FirstName = "Sheila",
            LastName = "Auteur"
        });
        builder.Entity<Author>().HasData(new Author() {
            AuthorId = 3,
            FirstName = "Authmanda",
            LastName = "Tor"
        });
        builder.Entity<Author>().HasData(new Author() {
            AuthorId = 4,
            FirstName = "Authin",
            LastName = "Eur"
        });
    }




    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;



}


