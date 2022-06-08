using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

//[Route("odata/[controller]")]
//[ApiController]
[ODataRouting]
public class BooksController : ControllerBase
{
    private BookStoreContext _db;

    public BooksController(BookStoreContext context)
    {
        _db = context;

        if (context!.Books!.Count() == 0)
        {
            foreach (var b in DataSource.GetBooks())
            {
                context.Books!.Add(b);
                context.Presses!.Add(b.Press!);
            }
            context.SaveChanges();
        }
    }

    [HttpGet("odata/Books")]
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_db.Books!);
    }

    // Returns a specific book given its key

    [HttpGet("odata/Books({key})")]
    [EnableQuery]
    public IActionResult GetById(int key)
    {
        return Ok(_db.Books!.FirstOrDefault(c => c.Id == key));
    }

    // Create a new book
    [HttpPost("odata/Books")]
    [EnableQuery]
    public IActionResult Post([FromBody] Book book)
    {
        _db.Books!.Add(book);
        _db.SaveChanges();

        return CreatedAtAction(nameof(GetById), new {Id = book.Id}, book);
    }
}


