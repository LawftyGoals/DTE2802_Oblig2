using BookAPI.Models;

namespace BookAPI.Service;
public interface IAuthorService {
    Task<List<Author>> GetAll();
    Task<Author?> Get(int id);
    Task Delete(int id);
    Task<Task> Save(Author Author);


}
