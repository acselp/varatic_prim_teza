using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Service.Services;

public class HashService : IHashService
{
    public string Hash(string password, string salt)
    {
        var byte_salt = Convert.FromBase64String(salt);
        
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: byte_salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }

    public string GenerateSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8));
    }
}