using System.Collections.Generic;

namespace Shop.Entities
{
    public class Album
    {
        /// <summary>
        /// Идентификатор альбома
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public short Year { get; set; }

        /// <summary>
        /// Ссылка на обложку альбома на Гугл Диске
        /// </summary>
        public string UrlToImg { get; set; }

        /// <summary>
        /// Описание альбома
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Описание альбома
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        /// <summary>
        /// Треки альбома
        /// </summary>
        public ICollection<Track> Tracks { get; set; }
    }
}
