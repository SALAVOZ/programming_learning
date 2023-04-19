using Microsoft.EntityFrameworkCore;

namespace Shop.DataBase
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }
        public ShopDbContext()
        {

        }
    }
}
