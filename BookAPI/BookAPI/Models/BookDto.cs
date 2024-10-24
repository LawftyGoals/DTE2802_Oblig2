namespace BookAPI.Models;
public class BookDto {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Year { get; set; } = 2024;
    public int AuthorId { get; set; }

    public Book toBook() => new Book { Title = this.Title, Description = this.Description, Year = this.Year, AuthorId = this.AuthorId };

}
