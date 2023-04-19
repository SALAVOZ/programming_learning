using System.Collections.Generic;

namespace Shop.DTOs
{
    public class GenresMenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FullAuthorsDTO> FullAuthors { get; set; }
    }

    public class FullAuthorsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
