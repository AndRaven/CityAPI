
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("/api/authentication")]
/// <summary>
/// The AuthenticationController handles user authentication by validating credentials, 
/// generating JWT tokens, and returning tokens on successful authentication.
/// </summary>
public class AuthenticationController : ControllerBase
{

  private IConfiguration _configuration;

  public AuthenticationController(IConfiguration configuration)
  {
    _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

  }

  [HttpPost]
  public ActionResult<string> Authenticate(AuthenticationRequestBody requestBody)
  {
    //validate user credentials
    var user = ValidateUserCredentials(requestBody.Username, requestBody.Password);

    if (user == null)
    {
      return Unauthorized();
    }

    //create the signing credentials
    var securitySigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
    var signingCredentials = new SigningCredentials(securitySigningKey, SecurityAlgorithms.HmacSha256);

    //creating claims
    var claims = new List<Claim>();
    claims.Add(new Claim("sub", user.UserId.ToString()));
    claims.Add(new Claim("given_name", user.FirstName));
    claims.Add(new Claim("family_name", user.LastName));
    claims.Add(new Claim("city", user.City));

    //create the JWT token

    var token = new JwtSecurityToken(_configuration["Authentication:Issuer"],
                                     _configuration["Authentication:Audience"],
                                     claims,
                                     DateTime.UtcNow,
                                     DateTime.UtcNow.AddHours(1),
                                     signingCredentials);

    /// Serializes the JWT token into a string and returns it
    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);

    return Ok(tokenToReturn);

  }

  private UserInfo ValidateUserCredentials(string username, string password)
  {
    // TODO: Validate credentials against data store but in this case we don't have a data store
    return new UserInfo(1,
    username ?? "",
    "Test",
    "Last",
    "Melbourne");
  }

}