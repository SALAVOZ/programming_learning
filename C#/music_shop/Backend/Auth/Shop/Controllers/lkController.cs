using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Shop.Attributes;
using Shop.Models;
using Shop.Models.lk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [AuthorizeRoles(Role.User)]
    [Route("api/[controller]")]
    public class lkController : Controller
    {
        private Guid UserId => Guid.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        [HttpGet]
        [AuthorizeRoles(Role.User)]
        [Route("purchase")]
        public async Task<IActionResult> GetPurchase()
        {
            var purchase_alboms = new List<lkAlbom>();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT albom.author, albom.title,albom.year, albom.img_path 
                                              FROM purchase
                                              JOIN albom ON purchase.albom_id = albom.id
                                              WHERE purchase.user_uuid = uuid('{UserId}');
                                              " ;
                    var reader = command.ExecuteReader();
                    while(await reader.ReadAsync())
                    {
                        purchase_alboms.Add(new lkAlbom
                        {
                            Author = reader.GetString(0),
                            Title = reader.GetString(1),
                            Year = reader.GetInt32(2),
                            Img_path = reader.GetString(3)
                        });
                    }
                }
            }
            return Ok(purchase_alboms);
        }



    }
}
