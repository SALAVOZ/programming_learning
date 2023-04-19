using Auth.Login.DataBase;
using Microsoft.AspNetCore.Mvc;
using Auth.Login.Models;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Options;
using Auth.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Auth.Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IdentityOptions _options = new IdentityOptions();
        private readonly AuthDbContext _dbContext;
        private readonly IOptions<AuthOptions> authOptions;
        public AuthController(AuthDbContext context, IOptions<AuthOptions> options)
            => (_dbContext, authOptions) = (context, options);

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Auth( [FromBody] AuthModel request)
        {
            var user = await Authenticate(request.email, request.password);
            if(user != null)
            {
                var token = GenerateToken(user);
                return Ok(new
                {
                    access_token = token
                });
            }
            return Unauthorized();
        }

        private async Task<Account> Authenticate(string email, string password)
        {
            using(var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"SELECT users.id, users.email, users.password, roles.role
                                             from users
                                             JOIN roles ON roles.id=users.role_id
                                             WHERE users.email='{email}' AND users.password='{password}'  ; ";
                    using (var reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            return new Account
                            {
                                UserId = reader.GetGuid(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        private string GenerateToken(Account user)
        {
            var authParams = authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(_options.ClaimsIdentity.RoleClaimType, user.Role)
            };


            //foreach (var role in user.Role)
            //{
              //  claims.Add(new Claim("role", role.ToString()));
            //}



            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.LifeTime),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Route("forgot/{email}")]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            string password;
            using (var connection = new NpgsqlConnection("server=localhost; port=5432; database=auth; username=postgres; password=salavat;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@" SELECT password from users WHERE email = '{email}' ";
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        password = reader.GetString(0);
                    }
                    else
                    {
                        return BadRequest();
                    }
                    await reader.CloseAsync();
                    MailAddress from = new MailAddress("salik.abdulin@gmail.com", "admin");
                    MailAddress to = new MailAddress(email);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "ПАРОЛЬ ОТ САЙТА IRET-SILA";
                    m.Body = $"Ваш пароль от сайта iret-sila {password}";
                    m.IsBodyHtml = false;

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.Credentials = new NetworkCredential("salik.abdulin@gmail.com", "ilikeshaurma");
                    client.EnableSsl = true;
                    await client.SendMailAsync(m);
                }
            }
            return Ok();
        }
    }
}
