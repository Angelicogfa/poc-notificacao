using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotificacaoAPI.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotificacaoAPI.Controllers
{
    [ApiController]
    [Route("api/authorization")]
    public class AuthorizationController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromServices] IConfiguration configuration, [FromBody] AutenticationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret:Hash"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return Ok(new { token, name=request.UserName });
        }
    }
}
