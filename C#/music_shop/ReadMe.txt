Для полноценной работы проекта необходимо создать в БД таблицы с жанрами, авторами, альбомами и треками
Все они создаются строго по порядку с помощью методов "create' соответствующих контроллеров:

1. GenresController -> https://localhost:5001/genres/create
2. AuthorsController -> https://localhost:5001/authors/create
3. AlbumsController -> https://localhost:5001/albums/create
4. TracksController -> https://localhost:5001/tracks/create

Запрос контроллера 1 можно выполнять сразу без изменений.

Для создания всех авторов первого жанра занесите в контроллер 2 следующую информацию:

string infoPath = "D:/MusicResources/Retro/AuthorsInformation.txt";
string imgPath =  "D:/MusicResources/Retro/AuthorsImages.txt";
List<string> names = new List<string> { "Al Bano & Romina Power" };
GenreId = 1
  
Для создания всех альбомов первого автора занесите в контроллер 3 следующую информацию:

string infoPath = "D:/MusicResources/Retro/Al Bano & Romina Power/Descriptions.txt"; 
string imgPath =  "D:/MusicResources/Retro/Al Bano & Romina Power/Images.txt"; 
List<string> names = new List<string> { "Aria Pura", "Felicidad", "Que Angel Sera" }; 
List<short> years = new List<short> { 1982, 1982, 1984 };
AuthorName = "Al Bano & Romina Power", 
AuthorId = 1
  
Для создания треков первого альбома занесите в контроллер 4 следующую информацию:

string namesPath = "D:/MusicResources/Retro/Al Bano & Romina Power/1982-AriaPura/AriaPuraNames.txt"; 
string urlsPath =  "D:/MusicResources/Retro/Al Bano & Romina Power/1982-AriaPura/AriaPuraURLs.txt"; 
AuthorName = "Al Bano & Romina Power",                  
AlbumId = 1
  
После внесения описанных изменений можете в строгом порядке выполнять методы, описанные в начале в пунктах 1 - 4 