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
    public class TracksController : Controller
    {

        private readonly MusicContext _context;

        public TracksController(MusicContext context)
        {
            _context = context;
        }

        [HttpGet("tracksList/{AlbumId}")]
        public async Task<ActionResult<IList<TracksListDTO>>> List(int AlbumId)
        {
            var tracksList = await _context.Albums
                .Where(x => x.Id == AlbumId)
                .Select(x => new TracksListDTO()
                {
                    Name = x.Name,
                    AuthorName = x.AuthorName,
                    AlbumImage = x.UrlToImg,
                    Description = x.Description,
                    AuthorId = x.AuthorId,
                    AlbumTracks = _context.Tracks
                        .Where(y => y.AlbumId == AlbumId)
                        .Select(y => new AlbumTracksDTO()
                        {
                            Id = y.Id,
                            Name = y.Name,
                            AuthorName = y.AuthorName,
                            UrlToDrive = y.UrlToDrive
                        })
                        .ToList()
                })
                .ToListAsync();
            return tracksList;
        }

        [HttpGet("create")]
        public async Task<ActionResult<IList<Track>>> CreateTracks()
        {
            //Чтобы записать треки в БД
            //Укажите путь к файлу с названиями треков
            string namesPath =
                "D:/MusicResources/OriginalPerformance/Elena Yerevan/The best of Elena Yerevan/TheBestOfNames.txt";

            //Укажите путь к файлу со ссылками на треки 
            string urlsPath =
                "D:/MusicResources/OriginalPerformance/Elena Yerevan/The best of Elena Yerevan/TheBestOfURLs.txt";

            List<string> names = DataReader.ReadData(namesPath);
            List<string> urls = DataReader.ReadData(urlsPath);

            List<Track> newTracks = new List<Track>();
            for (int i = 0; i < names.Count; i++)
            {
                var newTrack = new Track()
                {
                    Name = names[i],
                    AuthorName = "Elena Yerevan",
                    UrlToDrive = urls[i],
                    AlbumId = 5
                };
                newTracks.Add(newTrack);
                _context.Add(newTrack);
            }

            await _context.SaveChangesAsync();

            return newTracks;
        }
    }
}
