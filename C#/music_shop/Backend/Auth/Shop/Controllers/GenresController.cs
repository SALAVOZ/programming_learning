using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DTOs;
using Shop.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController : Controller
    {
        private readonly MusicContext _context;

        public GenresController(MusicContext context)
        {
            _context = context;
        }

        [HttpGet("genresMenu")]
        public async Task<ActionResult<IList<GenresMenuDTO>>> getGenresMenu()
        {
            var genresMenu = await _context.Genres
                .Select(x => new GenresMenuDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    FullAuthors = _context.Authors
                        .Where(y => y.GenreId == x.Id)
                        .Select(y => new FullAuthorsDTO()
                        {
                            Id = y.Id,
                            Name = y.Name
                        }).ToList()
                }).ToListAsync();
            return genresMenu;
        }

        [HttpGet("create")]
        public async Task<ActionResult<IList<Genre>>> CreateGenre()
        {
            List<string> names = new List<string>() { "Ретро", "Оригинальное исполнение" };

            List<Genre> genres = new List<Genre>();
            for (int i = 0; i < names.Count; i++)
            {
                var newGenre = new Genre()
                {
                    Name = names[i]
                };
                genres.Add(newGenre);
                _context.Add(newGenre);
            }

            await _context.SaveChangesAsync();

            return genres;
        }
    }
}
