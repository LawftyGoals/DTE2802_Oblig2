using BookAPI.Models;

namespace BookAPI.Service;
public class AuthorService : IAuthorService {

    private static List<Author> Authors { get; set; }
    private static int _nextId = 5;

    static AuthorService() {

        Authors = new List<Author>() {
            new Author() {
                AuthorId = 1,
                FirstName = "Jack",
                LastName = "Author"
            },
            new Author() {
                AuthorId = 2,
                FirstName = "Sheila",
                LastName = "Auteur"
            },
            new Author() {
                AuthorId = 3,
                FirstName = "Authmanda",
                LastName = "Tor"
            },
            new Author() {
                AuthorId = 4,
                FirstName = "Authin",
                LastName = "Eur"
            },
        };

    }

    public async Task<List<Author>> GetAll() => await Task.FromResult(Authors);

    public async Task<Author?> Get(int id) => await Task.FromResult(Authors.FirstOrDefault(b => b.AuthorId == id));

    public async Task Delete(int id) {
        Author author = await Get(id);
        Authors.Remove(author);
        await Task.Yield();

    }

    public async Task<Task> Save(Author author) {
        var existingAuthor = await Get(author.AuthorId);

        if (existingAuthor != null) {
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
        }
        else {
            author.AuthorId = _nextId;
            _nextId++;
            Authors.Add(author);
        }


        return Task.CompletedTask;
    }


}
