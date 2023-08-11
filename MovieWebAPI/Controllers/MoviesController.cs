using Microsoft.AspNetCore.Mvc;
using MovieWebAPI.Data;
using MovieWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<MoviesController>
        [HttpGet]
        public IActionResult Get()
        {
            var allMovies = _context.Movies.ToList();
            return Ok(allMovies);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return StatusCode(201,movie);
            }
            return BadRequest(ModelState);
            
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            var movieToUpdate = _context.Movies.Find(id);
            if (movieToUpdate == null)
            {
                return NotFound();
            }
            movieToUpdate.Title = movie.Title;
            movieToUpdate.RunningTime = movie.RunningTime;
            movieToUpdate.Genre = movie.Genre;
            _context.Movies.Update(movieToUpdate);
            _context.SaveChanges();
            return Ok(movieToUpdate);
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
