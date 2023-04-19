using System.Collections.Generic;

namespace Shop.DTOs
{
    public class TracksListDTO
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string AlbumImage { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public ICollection<AlbumTracksDTO> AlbumTracks { get; set; }
    }

    public class AlbumTracksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string UrlToDrive { get; set; }
    }
}
