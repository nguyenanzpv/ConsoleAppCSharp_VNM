using Ecommerce.IdentityJWT.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;//claims la cac thanh phan de xac dinh duoc 1 doi tuong
using System.Text;

namespace Ecommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager; //give apis manage user
    private readonly RoleManager<IdentityRole> _roleManager; //give apis manage role
    private readonly IConfiguration _configuration; // read file settings

    //ctrl + . to generate constructor
    public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    //register user has role (admin)
    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterModel model)
    {
        //1. check exist user
        var userExist = await this._userManager.FindByNameAsync(model.UserName);
        if (userExist != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHeader { Status = "Error", Message = "User already exists" });
        }
        //2. create new identity user
        IdentityUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName
        };
        //3. apply into db via userManager
        var result = await this._userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHeader { Status = "Error", Message = "Create User Identity failed, please check user infornation input again" });
        }
        //4. if register ok -> add role(Admin,User,Clinet) to table Role
        if(!await this._roleManager.RoleExistsAsync(UserRoles.AdminRole))
        {
            await this._roleManager.CreateAsync(new IdentityRole(UserRoles.AdminRole));
        }
        if (!await this._roleManager.RoleExistsAsync(UserRoles.UserRole))
        {
            await this._roleManager.CreateAsync(new IdentityRole(UserRoles.UserRole));
        }
        if (!await this._roleManager.RoleExistsAsync(UserRoles.ClientRole))
        {
            await this._roleManager.CreateAsync(new IdentityRole(UserRoles.ClientRole));
        }
        //5. add user to each role
        if(await this._roleManager.RoleExistsAsync(UserRoles.AdminRole))
        {
            await this._userManager.AddToRoleAsync(user,UserRoles.AdminRole);
        }
        if (await this._roleManager.RoleExistsAsync(UserRoles.UserRole))
        {
            await this._userManager.AddToRoleAsync(user, UserRoles.UserRole);
        }
        if (await this._roleManager.RoleExistsAsync(UserRoles.ClientRole))
        {
            await this._userManager.AddToRoleAsync(user, UserRoles.ClientRole);
        }
        //6. return user
        return Ok(new ResponseHeader { Status="Success", Message="User created successfully"});

    }

    //register user
    [HttpPost]
    [Route("register-user")]
    public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
    {
        //1. check exist user
        var userExist = await this._userManager.FindByNameAsync(model.UserName);
        if (userExist != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHeader { Status="Error", Message="User already exists"});
        }
        //2. create new identity user
        IdentityUser user = new() { 
            Email=model.Email,
            SecurityStamp=Guid.NewGuid().ToString(),
            UserName=model.UserName
        };

        //3. apply db via UserManager
        var result = await this._userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHeader { Status = "Error", Message = "Create User Identity failed, please check user infornation input again" });
        }
        return Ok(new ResponseHeader { Status="Success", Message="Created User Identity success"});
    }

    //login and generated token
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> UserLogin([FromBody] UserLoginModel model)
    {
        //check exist user
        var user = await this._userManager.FindByNameAsync(model.UserName);
        if(user != null && await this._userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await this._userManager.GetRolesAsync(user);//get all role of user
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),//xac thuc User qua UserName
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//xac thuc qua json web token

            };

            //2. if user has role -> add role to claim
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            //3. generated token
            var tokenValue = GeneratedToken(authClaims);
            //4. return ok to browser
            return Ok(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(tokenValue),
                    expiration = tokenValue.ValidTo //thoi han token
                }
            );
        }
       return Unauthorized();// ko duoc xac thuc
    }

    //function render token
    private JwtSecurityToken GeneratedToken(List<Claim> authClaims)
    {
        //1. convert private key to byte array from appsettings
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Secret"]));//secret key
        //2. render token value
        var tokenValue = new JwtSecurityToken(
            issuer: this._configuration["JWT:ValidIssuer"],
            audience: this._configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(2),
            claims: authClaims,//info sign
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256) //giai thuat singing
        );
        return tokenValue;
    }
}
