
using System.Security.Cryptography;
using System.Text;

public class StringHelper
{
    public static string GetPasswordHash(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            // Send a sample text to hash.
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            // Get the hashed string.
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash;
        }
    }
    public static string GetRandomToken(int length)
    {
        var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var resultToken = new string(
           Enumerable.Repeat(allChar, length)
           .Select(token => token[random.Next(token.Length)]).ToArray());

        return resultToken.ToString();
    }
}