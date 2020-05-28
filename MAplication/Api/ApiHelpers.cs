using MAplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MAplication.Api
{
    public class ApiHelpers
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ApiHelpers(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> GenerateToken(string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            var someClaims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, userName), new Claim(JwtRegisteredClaimNames.Email, userName) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(someClaims, "Token");
            // Adding roles code
            // Roles property is string collection but you can modify Select code if it it's not
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("worldenderaatrox00"));
            var token = new JwtSecurityToken(
                audience: "worldenderaatrox.com",
                issuer: "worldenderaatrox.com",
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
