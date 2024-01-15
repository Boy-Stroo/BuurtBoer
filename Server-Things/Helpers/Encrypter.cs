using System.Security.Cryptography;
using System.Text;

static class Encrypter
{
    public static string Encrypt(string text)
    {
        var data = Encoding.UTF8.GetBytes(text);
        using var sha1 = SHA1.Create();
        var hash = sha1.ComputeHash(data);
        return Encoding.UTF8.GetString(hash);
    }
}
