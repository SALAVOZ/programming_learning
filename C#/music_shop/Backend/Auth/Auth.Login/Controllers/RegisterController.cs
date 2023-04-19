using Auth.Login.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Register([FromBody]RegisterModel request)
        {
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT email FROM users 
                                              WHERE email = '{request.email}'  ";
                    var reader = command.ExecuteReader();
                    if (reader.Read()) return BadRequest();
                    await reader.CloseAsync();
                    command.CommandText = $@" INSERT INTO users (email, password) VALUES ( '{request.email}' , '{request.password}'   ); " ;
                    await command.ExecuteNonQueryAsync(); 
                }
            }
            return Ok();
        }
    }
}
