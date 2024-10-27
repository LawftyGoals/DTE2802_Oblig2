using BookAPI.Models.Dtos;
using BookAPI.Models.Entities;
using BookAPI.Service.AuthorServices;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[Route("/api/[controller]")]
public class AuthorController : Controller {

    readonly IAuthorService _service;

    public AuthorController(IAuthorService authorService) {
        _service = authorService;
    }

    [HttpGet]
    public async Task<List<Author>> GetAll() => await _service.GetAll();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id) {
        var author = await _service.Get(id);
        if (author == null) return NotFound($"Author with id: {id} has not been found");
        return Ok(author);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var author = await _service.Get(id);
        if (author == null) return NotFound($"Author with id: {id} has not been found");
        await _service.Delete(id);
        return Ok($"Author with id: {id} has been deleted");

    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AuthorDto target) {
        if (target == null) return BadRequest("Empty object can not be created");

        var author = target.ToAuthor();


        await _service.Save(author);

        return CreatedAtAction(nameof(Get), new { id = author.AuthorId }, author);

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Author author) {
        if (id != author.AuthorId) return BadRequest("Provided id does not match products id");

        var existingAuthor = _service.Get(id);

        if (existingAuthor is null) return NotFound($"Author with id {id} has not been found");

        await _service.Save(author);

        return Ok($"Author {author.AuthorId} - {author.FirstName} {author.LastName} has been updated successfully");
    }


}
