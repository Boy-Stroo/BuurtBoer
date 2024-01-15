static class Encrypter
{
    public static string Encrypt(string text) => BCrypt.Net.BCrypt.HashPassword(text);
}
