namespace Shop.Entities
{
    public class Single
    {
        /// <summary>
        /// Идентификатор сингла
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название сингла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Прямая ссылка на скачивание файла из Гугл Диска
        /// </summary>
        public string UrlToDrive { get; set; }

        /// <summary>
        /// Идентификатор альбома
        /// </summary>
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
