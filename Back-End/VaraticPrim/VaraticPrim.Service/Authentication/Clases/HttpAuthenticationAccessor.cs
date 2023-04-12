using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Service.Authentication;

internal class HttpAuthenticationAccessor : IAuthenticationAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserRepository         _userRepository;
    private readonly Lazy<int>            _userId;
    private          IEnumerable<Claim>   Claims => _contextAccessor.HttpContext.User.Claims;
    private          UserEntity?          _identity;
 
    public HttpAuthenticationAccessor(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
    {
        _contextAccessor = contextAccessor;
        _userRepository = userRepository;
        _userId = new Lazy<int>(() => GetIntClaim(ClaimsTypes.UserId));
    }
 
    private int GetIntClaim(string key)
    {
        var claim = Claims.FirstOrDefault(it => it.Type == key);
        var value = Convert.ToInt32(claim?.Value);
 
        return value;
    }
 
    public int UserId => _userId.Value;
 
    public async Task<UserEntity?> LoggedIdentity()
    {
        return _identity ??= await _userRepository.GetById(UserId);
    }
}