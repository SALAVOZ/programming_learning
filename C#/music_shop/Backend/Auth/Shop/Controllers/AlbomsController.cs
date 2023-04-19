using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Shop.Attributes;
using Shop.Models;
using Shop.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbomsController : Controller
    {
        private Guid UserId => Guid.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);


        [HttpGet("{str}")]
        [AuthorizeRoles(Role.User, Role.Seller)]
        [Route("")]
        public async Task<IActionResult> GetCatalog(string str)
        {
            var catalog = new List<Albom>();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    if (str == string.Empty || str == null)
                    { 
                        command.CommandText = @$" SELECT albom.id, albom.author, albom.title, albom.year, albom.img_path, albom.price
                                                    FROM albom
                                                    limit 12";
                    }
                    else
                    {
                        str.Trim(' ');
                        command.CommandText = @$" SELECT albom.id, albom.author, albom.title, albom.year, albom.img_path, albom.price
                                                    FROM albom
                                                    where title like '%{str}%' ;"    ;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            catalog.Add(new Albom
                            {
                                Albom_id = reader.GetInt32(0),
                                Author = reader.GetString(1),
                                Title = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                Img_path = reader.GetString(4),
                                Price = reader.GetInt32(5)
                            }
                                      );
                        }
                    }
                }
            }
            return Ok(catalog);
        }


        [HttpPost]
        [Authorize]
        [Route("add")]
        public async Task<IActionResult> AddOrder([FromBody]AddingModel model)
        {

            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                        command.CommandText = @$" SELECT * FROM card WHERE user_uuid = uuid('{UserId.ToString()}') AND albom_id = {model.Id}; ";
                        var reader = command.ExecuteReader();
                        if (await reader.ReadAsync()) return NoContent();
                        reader.Close();
                        command.CommandText = @$" INSERT INTO card (user_uuid, albom_id)
                                                    VALUES ( uuid('{UserId.ToString()}'), {model.Id} ); ";
                        await command.ExecuteNonQueryAsync();
                }
            }
            return NoContent();
        }

    }
}
