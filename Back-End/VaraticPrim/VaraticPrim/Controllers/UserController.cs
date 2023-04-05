using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Models.UserModels;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]/{id?}")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<UserEntity> Test()
    {
        return await _userRepository.GetById(1);
    }

    [HttpPost]
    public async Task<UserModel> Create([FromBody] UserCreateModel userModel)
    {
        UserEntity userEntity = _mapper.Map<UserEntity>(userModel);
        
        await _userRepository.Insert(userEntity);
        
        return _mapper.Map<UserModel>(userEntity);
    }
}