namespace BookAPI.Models;
public class Book {
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Year { get; set; } = 2024;
    public int AuthorId { get; set; }

}
