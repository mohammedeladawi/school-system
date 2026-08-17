using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Application.Helpers;

public static class Utils
{
    public static string Hash(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    public static string Encode(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var base64 = Convert.ToBase64String(bytes);
        // Convert to Base64 URL-safe string (RFC 4648 §5)
        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    public static string Decode(string encodedInput)
    {
        var incoming = encodedInput;
        // Restore padding
        var mod4 = incoming.Length % 4;
        if (mod4 != 0)
            incoming += new string('=', 4 - mod4);
        var base64 = incoming.Replace('-', '+').Replace('_', '/');
        var bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }

    public static string IdentityErrorsFormater(IEnumerable<IdentityError> errors)
    {
        return string.Join(" ", errors.Select(e => e.Description));


    }
}
