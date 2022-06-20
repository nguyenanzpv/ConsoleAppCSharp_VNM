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

    //login
}
