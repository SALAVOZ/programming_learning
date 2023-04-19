using System.Collections.Generic;

namespace Shop.DTOs
{
    public class AuthorWithFullAlbumsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FullAlbumsDTO> FullAlbums { get; set; }
    }

    public class FullAlbumsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Year { get; set; }
        public string UrlToImg { get; set; }
    }
}
