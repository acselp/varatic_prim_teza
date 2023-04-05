using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]/{id?}")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<UserEntity> Test()
    {
        return await _userRepository.GetById(1);
    }

    [HttpPost]
    public async Task<UserEntity> Create([FromBody] UserEntity user)
    {
        //MODEL FROM POSTMAN MAP TO ENTITY
        //MAP TO USERMODEL
        await _userRepository.Insert(user);

        return user;
    }
}