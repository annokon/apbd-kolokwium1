using kolokwium.Models.DTOs;
using kolokwium.Repository;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;
 
    public BooksController(IBookRepository bookRepository, IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _genreRepository = genreRepository;
    }
 
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        var addedBook = await _bookRepository.AddAsync(book);
        return CreatedAtAction(nameof(GetGenresForBook), new { id = addedBook.Id }, addedBook);
    }
 
    [HttpGet("{id}/genres")]
    public async Task<ActionResult<IEnumerable<string>>> GetGenresForBook(int id)
    {
        var genres = await _genreRepository.GetGenresForBookAsync(id);
        if (!genres.Any())
        {
            return NotFound();
        }
 
        return Ok(genres);
    }
}