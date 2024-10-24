using BookAPI.Models;
using BookAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[Route("/api/[controller]")]
public class BookController : Controller {

    readonly IBookService _service;

    public BookController(IBookService bookService) {
        _service = bookService;
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

        var book = target.toBook();


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
