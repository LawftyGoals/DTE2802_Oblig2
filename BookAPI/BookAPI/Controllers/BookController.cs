using BookAPI.Models.Dtos;
using BookAPI.Models.Entities;
using BookAPI.Service.AuthorServices;
using BookAPI.Service.BookServices;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[Route("/api/[controller]")]
public class BookController : Controller {

    readonly IBookService _service;
    readonly IAuthorService _authorService;

    public BookController(IBookService bookService, IAuthorService authorService) {
        _service = bookService;
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<List<Book>> GetAll() => await _service.GetAll();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id) {
        var book = await _service.Get(id);
        if (book == null) return NotFound($"Book with id: {id} has not been found");
        return Ok(book);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var book = await _service.Get(id);
        if (book == null) return NotFound($"Book with id: {id} has not been found");
        await _service.Delete(id);
        return Ok($"Book with id: {id} has been deleted");

    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] BookDto target) {
        if (target == null) return BadRequest("Empty object can not be created");


        var author = (await _authorService.GetAll()).FirstOrDefault(a => a.FirstName == target.AuthorFirstName && a.LastName == target.AuthorLastName);

        var existingBook = (await _service.GetAll()).FirstOrDefault(b => b.Title == target.Title && b.AuthorId == author.AuthorId);

        if (existingBook != null) return BadRequest("Book Already Exists");

        if (author == null) {

            await _authorService.Save(new() { FirstName = target.AuthorFirstName, LastName = target.AuthorLastName });

            author = (await _authorService.GetAll()).First(a => a.FirstName == target.AuthorFirstName && a.LastName == target.AuthorLastName);
        }

        var book = new Book() { Title = target.Title, Description = target.Description, Year = target.Year, AuthorId = author.AuthorId };

        await _service.Save(book);

        return CreatedAtAction(nameof(Get), new { id = book.BookId }, book);

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Book book) {
        if (id != book.BookId) return BadRequest("Provided id does not match products id");

        var existingBook = _service.Get(id);

        if (existingBook is null) return NotFound($"Book with id {id} has not been found");

        await _service.Save(book);

        return Ok($"Book {book.BookId} - {book.Title} has been updated successfully");
    }


}
