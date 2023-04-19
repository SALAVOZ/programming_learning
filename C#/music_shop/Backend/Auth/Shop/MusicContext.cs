using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop
{
    public class MusicContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public MusicContext()
        {
            Database.EnsureCreated();
        }

        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {

        }
    }
}
