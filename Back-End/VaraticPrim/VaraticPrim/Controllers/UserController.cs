using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<UserEntity> Test()
    {
        return _userRepository.GetUser(1);
    }

    [HttpPost]
    public async Task<UserEntity> Create(UserEntity user)
    {
        _userRepository.Add(user);

        return user;
    }
}