using BookAPI.Models;

namespace BookAPI.Service;
public class BookService : IBookService {

    private static List<Book> Books { get; set; }
    private static int _nextId = 5;

    static BookService() {

        Books = new List<Book>() {
            new Book() {
                BookId = 1,
                Title = "A Great Book",
                Description = "One for the ages.",
                Year = 2024,
                AuthorId = 1
            },
            new Book() {
                BookId = 2,
                Title = "A Great Book, part 2",
                Description = "This one is not so great.",
                Year = 2024,
                AuthorId = 1
            },
            new Book() {
                BookId = 3,
                Title = "A Silly Book",
                Description = "A silly book indeed.",
                Year = 2024,
                AuthorId = 2
            },
            new Book() {
                BookId = 4,
                Title = "Alpha et Omega al.",
                Description = "So dense its stupidly briliant.",
                Year = 2024,
                AuthorId = 3
            },
        };

    }

    public async Task<List<Book>> GetAll() => await Task.FromResult(Books);

    public async Task<Book?> Get(int id) => await Task.FromResult(Books.FirstOrDefault(b => b.BookId == id));

    public async Task Delete(int id) {
        Book book = await Get(id);
        Books.Remove(book);
        await Task.Yield();

    }

    public async Task<Task> Save(Book book) {
        var existingBook = await Get(book.BookId);

        if (existingBook != null) {
            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.Year = book.Year;
            existingBook.AuthorId = book.AuthorId;
        }
        else {
            book.BookId = _nextId;
            _nextId++;
            Books.Add(book);
        }


        return Task.CompletedTask;
    }


}
