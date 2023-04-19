using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Shop.Attributes;
using Shop.Models;
using Shop.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizeRoles(Role.Admin)]
    public class AdminController : Controller
    {
        private Guid UserId => Guid.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        [HttpGet]
        [Route("")]
        [AuthorizeRoles(Role.Admin)]
        public IActionResult Index()
        {
            string role = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
            if (role == "admin")
                return Ok();
            else return Forbid();
        }

        [HttpGet]
        [AuthorizeRoles(Role.Admin)]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = new List<UserModelAdminPage>();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT users.id, users.email, roles.role
                                              FROM users
                                              JOIN roles ON users.role_id = roles.id
                                              WHERE roles.role != 'admin';             ";
                    var reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        users.Add(new UserModelAdminPage
                        {
                            UserId = reader.GetGuid(0).ToString(),
                            Email = reader.GetString(1),
                            Role = reader.GetString(2)
                        });
                    }
                }
            }
            return Ok(users);
        }

        [HttpGet]
        [AuthorizeRoles(Role.Admin)]
        [Route("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var titles = new List<string>();
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT role FROM roles
                                              WHERE role != 'admin' ;  ";
                    var reader = command.ExecuteReader();
                    while(await reader.ReadAsync())
                    {
                        titles.Add(reader.GetString(0));
                    }
                }
            }
            return Ok(titles);
        }


        [HttpPost]
        [AuthorizeRoles(Role.Admin)]
        [Route("change")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleModel request)
        {
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" UPDATE users
                                              SET role_id = (SELECT id from roles where role = '{request.Role}')
                                              WHERE id = uuid('{request.UserId}');  ";
                    await command.ExecuteNonQueryAsync();
                }
            }
            return Ok();
        }
    }
}
