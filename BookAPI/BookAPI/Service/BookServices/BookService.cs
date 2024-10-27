using BookAPI.Data;
using BookAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service.BookServices;
public class BookService : IBookService {

    private readonly BookDbContext _db;

    public BookService(BookDbContext db) {

        _db = db;

    }

    public async Task<List<Book>> GetAll() {
        try {
            var books = await _db.Books.ToListAsync();
            return books;
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new List<Book>();
        }
    }

    public async Task<Book?> Get(int id) {
        try {
            var book = await _db.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
            return book;
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    public async Task Delete(int id) {
        var book = _db.Books.FindAsync(id);
        _db.Books.Remove(await book ?? throw new InvalidOperationException($"No computer with id {id} found."));
        await _db.SaveChangesAsync();

    }

    public async Task Save(Book book) {
        var existingBook = await _db.Books.FindAsync(book.BookId);

        if (existingBook != null) {
            _db.Entry(existingBook).State = EntityState.Detached;
        }

        _db.Books.Update(book);

        await _db.SaveChangesAsync();

    }


}
