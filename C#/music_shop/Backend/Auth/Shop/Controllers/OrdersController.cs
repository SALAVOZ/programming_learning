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
    public class OrdersController : Controller
    {
        private Guid UserId => Guid.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);


        [HttpGet]
        [AuthorizeRoles(Role.User)]
        [Route("")]
        public async Task<IActionResult> GetCards()
        { 
            var orders = new List<Card>();
            string UserIdString = UserId.ToString();
            using(var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = @$" SELECT card.id, albom.id, albom.author, albom.title, albom.year, albom.img_path, albom.price
                                                FROM card
                                                JOIN albom ON card.albom_id=albom.id
                                                JOIN users ON card.user_uuid=users.id
                                                WHERE card.user_uuid=uuid(  '{UserIdString}' ) ;   ";
                    using(var reader = command.ExecuteReader())
                    {
                        while(await reader.ReadAsync())
                        {
                            orders.Add(new Card 
                                        {
                                            Id = reader.GetInt32(0),
                                            Albom_id = reader.GetInt32(1),
                                            Author = reader.GetString(2),
                                            Title = reader.GetString(3),
                                            Year = reader.GetInt32(4),
                                            Img_path = reader.GetString(5),
                                            Price = reader.GetInt32(6)
                                        }
                                      );
                        }
                    }
                }
            }
            return Ok(orders);
        }


        [HttpDelete("delete/{id}")]
        [AuthorizeRoles(Role.User)]
        public async Task<IActionResult> DeleteCard(int id)
        {
            string UserIdString = UserId.ToString();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$" DELETE FROM card
                                              WHERE user_uuid=uuid(  '{UserIdString}' ) AND albom_id={id} ;   ";
                    await command.ExecuteNonQueryAsync();
                }
            }
            return NoContent();
        }

        [HttpGet]
        [AuthorizeRoles(Role.User)]
        [Route("buy")]
        public async Task<IActionResult> Buy()
        {
            var albom_ids = new List<int>();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT albom_id from card
                                              WHERE user_uuid = uuid('{UserId.ToString()}') ;  ";
                    var reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        albom_ids.Add(reader.GetInt32(0));
                    }
                    reader.Close();
                    if (albom_ids.Count == 0) return BadRequest();
                    foreach (var i in albom_ids)
                    {
                        command.CommandText = $@" INSERT INTO purchase (user_uuid, albom_id)
                                              VALUES ( uuid('{UserId}'),  {i}  )";
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            return Ok();
        }
    }
}
