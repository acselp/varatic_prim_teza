using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Models.UserModels;
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
    public async Task<UserEntity> Test([FromRoute] int id)
    {
        return await _userRepository.GetById(id);
    }

    [HttpPost]
    public async Task<UserModel> Create([FromBody] UserCreateModel userModel)
    {
        var userEntity = _mapper.Map<UserEntity>(userModel);
        
        await _userRepository.Insert(userEntity);
        
        return _mapper.Map<UserModel>(userEntity);
    }
}