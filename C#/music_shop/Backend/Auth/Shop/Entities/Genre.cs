using System.Collections.Generic;

namespace Shop.Entities
{
    public class Genre
    {
        /// <summary>
        /// Идентификатор жанра
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название жанра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Авторы жанра
        /// </summary>
        public ICollection<Author> Authors { get; set; }
    }
}
