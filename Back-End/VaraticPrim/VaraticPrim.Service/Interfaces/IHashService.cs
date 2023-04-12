namespace VaraticPrim.Service.Interfaces;

public interface IHashService
{
    string Hash(string password, string salt);
    string GenerateSalt();
}