namespace BookAPI.Models.Dtos;
public class BookDto {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Year { get; set; } = 2024;
    public string AuthorFirstName { get; set; } = null!;
    public string AuthorLastName { get; set; } = null!;

}
