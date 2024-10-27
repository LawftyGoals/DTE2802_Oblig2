using BookAPI.Models.Entities;

namespace BookAPI.Models.Dtos;
public class AuthorDto {
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Author ToAuthor() {
        return new Author { FirstName = FirstName, LastName = LastName };
    }

}
