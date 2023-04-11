using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Security;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Exceptions;
using VaraticPrim.Models.ContactModels;
using VaraticPrim.Models.LoginModel;
using VaraticPrim.Models.UserModels;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]/{id?}")]
public class LoginController : ApiBaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateModel> _userValidator;
    private readonly IValidator<ContactCreateModel> _contactValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;
    
    public LoginController(
        IUserRepository userRepository, 
        IMapper mapper, 
        IValidator<UserCreateModel> userValidator, 
        IValidator<ContactCreateModel> contactValidator, 
        ILogger<UserController> logger,
        IConfiguration config)
    {
        _config = config;
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var user = Authenticate(loginModel);

        if (user != null)
        {
            var token = Generate(user);

            return Ok(token);
        }

        return NotFound("User not found");
    }

    private string Generate(UserModel user)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email),
        };

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Jwt:Issuer"),
            _config.GetValue<string>("Jwt:Audience"),
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel Authenticate(LoginModel loginModel)
    {
        var currentUserEntity = _userRepository.GetByEmail(loginModel.Email);
        
        if(currentUserEntity != null)
            if (currentUserEntity.PasswordHash == loginModel.Password)
            {
                var userModel = _mapper.Map<UserModel>(currentUserEntity);
                
                return userModel;
            }
        
        return null;
    }
}