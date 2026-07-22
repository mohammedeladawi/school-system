using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Shared.Helpers;

public static class Utils
{
    public static string Hash(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
