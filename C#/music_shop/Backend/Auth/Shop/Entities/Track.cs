namespace Shop.Entities
{
    public class Track
    {
        /// <summary>
        /// Идентификатор трека
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название трека
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя автора / исполнителя
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Прямая ссылка на скачивание файла из Гугл Диска
        /// </summary>
        public string UrlToDrive { get; set; }

        /// <summary>
        /// Идентификатор альбома
        /// </summary>
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
