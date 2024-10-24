namespace BookAPI.Models;
public class AuthorDto {
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Author toAuthor() {
        return new Author { FirstName = this.FirstName, LastName = this.LastName };
    }

}
