using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DTOs;
using Shop.Entities;
using Shop.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly MusicContext _context;

        public AuthorsController(MusicContext context)
        {
            _context = context;
        }

        [HttpGet("albumsList/{AuthorId}")]
        public async Task<ActionResult<IList<AuthorWithFullAlbumsDTO>>> List(int AuthorId)
        {
            var authorsAlbums = await _context.Authors
                .Where(n => n.Id == AuthorId)
                .Select(x => new AuthorWithFullAlbumsDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    FullAlbums = _context.Albums
                        .Where(p => p.AuthorId == x.Id)
                        .Select(q => new FullAlbumsDTO
                        {
                            Id = q.Id,
                            Name = q.Name,
                            Year = q.Year,
                            UrlToImg = q.UrlToImg
                        })
                        .ToList()
                })
                .ToListAsync();

            return authorsAlbums;
        }

        [HttpGet("create")]
        public async Task<ActionResult<IList<Author>>> CreateAuthor()
        {
            // Для записи авторов данного жанра в БД
            // укажите пути до файлов с описаниями и фотографиями

            string infoPath =
                "D:/MusicResources/OriginalPerformance/AuthorsInformation.txt";
            string imgPath =
                "D:/MusicResources/OriginalPerformance/AuthorsImages.txt";

            List<string> imgs = DataReader.ReadData(imgPath);
            List<string> infos = DataReader.ReadData(infoPath);
            List<string> names = new List<string> { "Elena Yerevan" };

            List<Author> newAuthors = new List<Author>();
            for (int i = 0; i < names.Count; i++)
            {
                var newAuthor = new Author
                {
                    Name = names[i],
                    Information = infos[i],
                    UrlToImg = imgs[i],
                    GenreId = 2
                };
                newAuthors.Add(newAuthor);
                _context.Add(newAuthor);
            }
            await _context.SaveChangesAsync();

            return newAuthors;
        }
    }
}
