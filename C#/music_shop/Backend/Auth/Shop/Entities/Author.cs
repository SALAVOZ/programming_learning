using System.Collections.Generic;

namespace Shop.Entities
{
    public class Author
    {
        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя автора / исполнителя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Информация об авторе
        /// </summary>
        public string Information { get; set; }

        /// <summary>
        /// Ссылка на фотографию автора на Гугл Диске
        /// </summary>
        public string UrlToImg { get; set; }

        /// <summary>
        /// Идентификатор жанра
        /// </summary>
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        /// <summary>
        /// Список альбомов
        /// </summary>
        public ICollection<Album> Albums { get; set; }
    }
}
