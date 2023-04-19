using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Shop.Attributes;
using Shop.Models;
using Shop.Models.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [AuthorizeRoles(Role.Seller)]
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
        private Guid UserId => Guid.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        [HttpGet]
        [Route("")]
        [AuthorizeRoles(Role.Seller)]
        public IActionResult Index()
        {
            string role = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
            if (role == "seller")
                return Ok();
            else return Forbid();
        }

        [HttpPost]
        [AuthorizeRoles(Role.Seller)]
        [Route("add")]
        public async Task<IActionResult> AddAlbom([FromBody]AddUpdateAlbomModelSeller request)
        {
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" INSERT INTO albom (author, title, year, img_path, price,music_path)
                                              VALUES(
                                              '{request.Author}',
                                              '{request.Title}',
                                               {request.Year},
                                              '{request.Img_path}',
                                               {request.Price},
                                              '{request.Music_path}'
                                                    ); ";
                    await command.ExecuteNonQueryAsync();
                }
            }
            return Ok();
        }

        [HttpPut]
        [AuthorizeRoles(Role.Seller)]
        [Route("update")]
        public async Task<IActionResult> UpdateAlbom([FromBody]AddUpdateAlbomModelSeller request)
        {
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"   UPDATE albom
                                                SET author = '{request.Author}',
                                                    year = {request.Year},
                                                    img_path = '{request.Img_path}',
                                                    price = {request.Price},
                                                    music_path = '{request.Music_path}'
                                                 WHERE title = '{request.Title}'; 
                                              ";
                    await command.ExecuteNonQueryAsync();
                }
            }
            return Ok();
        }



        [HttpDelete]
        [AuthorizeRoles(Role.Seller)]
        [Route("delete/{title}")]
        public async Task<IActionResult> DeleteAlbom(string title)
        {
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" DELETE FROM albom
                                              WHERE title = '{title}';";
                    await command.ExecuteNonQueryAsync();
                }
            }
            return Ok();
        }

        [HttpGet]
        [AuthorizeRoles(Role.Seller)]
        [Route("titles")]
        public async Task<IActionResult> GetTitles()
        {
            var titles = new List<string>();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT title FROM albom;   ";
                    var reader = command.ExecuteReader();
                    while(await reader.ReadAsync())
                    {
                        titles.Add(reader.GetString(0));
                    }
                }
            }
            return Ok(titles);
        }

        [HttpGet]
        [AuthorizeRoles(Role.Seller)]
        [Route("info/{title}")]
        public IActionResult GetInfo(string title)
        {
            if (title == String.Empty || title == null) return BadRequest();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT author, year, img_path, price, music_path
                                              FROM albom
                                              WHERE title = '{title}' ;";
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        return Ok(new
                        {
                            Author = reader.GetString(0),
                            Year = reader.GetInt32(1),
                            Img_path = reader.GetString(2),
                            Price = reader.GetInt32(3),
                            Music_path = reader.GetString(4)
                        });
                    }
                }
            }
            return BadRequest();
        }











    }
}
