using System.Security.Cryptography;
using System.Text;

namespace Sozluk.Common.Infrastructure;

public class PasswordEncrytor
{
    public static string Encrpt(string password)
    {
        using var md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] hashByte = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashByte);
    }
}