using BookAPI.Models.Entities;


namespace BookAPI.Service.BookServices;
public interface IBookService {
    Task<List<Book>> GetAll();
    Task<Book?> Get(int id);
    Task Delete(int id);
    Task<Task> Save(Book book);

}
