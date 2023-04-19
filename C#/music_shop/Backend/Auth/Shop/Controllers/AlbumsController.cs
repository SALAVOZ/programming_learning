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
    public class AlbumsController : Controller
    {
        private readonly MusicContext _context;

        public AlbumsController(MusicContext context)
        {
            _context = context;
        }

        [HttpGet("authorInfo/{AuthorId}")]
        public async Task<ActionResult<IList<AuthorPageDTO>>> AuthorInfo(int AuthorId)
        {
            var authorInfo = await _context.Authors
                .Where(x => x.Id == AuthorId)
                .Select(x => new AuthorPageDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Information = x.Information,
                    UrlToImg = x.UrlToImg
                })
                .ToListAsync();

            return authorInfo;
        }

        [HttpGet("create")]
        public async Task<ActionResult<IList<Album>>> CreateAlbum()
        {
            // Для записи альбомов в БД
            // укажите пути до файлов с их описаниями и фотографиями

            string infoPath =
                "D:/MusicResources/OriginalPerformance/Elena Yerevan/Descriptions.txt";

            string imgPath =
                "D:/MusicResources/OriginalPerformance/Elena Yerevan/Images.txt";

            List<string> imgs = DataReader.ReadData(imgPath);
            List<string> infos = DataReader.ReadData(infoPath);
            List<string> names = new List<string> { "The best of Elena Yerevan" };
            List<short> years = new List<short> { 2017 };

            List<Album> newAlbums = new List<Album>();
            for (int i = 0; i < names.Count; i++)
            {
                var newAlbum = new Album
                {
                    Name = names[i],
                    Year = years[i],
                    UrlToImg = imgs[i],
                    AuthorName = "Elena Yerevan",
                    Description = infos[i],
                    AuthorId = 3
                };
                newAlbums.Add(newAlbum);
                _context.Add(newAlbum);
            }

            await _context.SaveChangesAsync();

            return newAlbums;
        }
    }
}
