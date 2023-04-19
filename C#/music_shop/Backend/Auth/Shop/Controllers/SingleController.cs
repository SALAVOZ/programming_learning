using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SinglesController : Controller
    {
        private readonly MusicContext _context;

        public SinglesController(MusicContext context)
        {
            _context = context;
        }

    }
}
