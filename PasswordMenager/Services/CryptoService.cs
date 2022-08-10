using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordMenager.Services;

public class CryptoService
{
    public static string GenerateSalt()
    {
        var saltBytes = new byte[255];

        using (var provider = new RNGCryptoServiceProvider())
        {
            provider.GetNonZeroBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }

    public static string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 256))
        {
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(255));
        }
    }

    public static string Encrypt(string textToEncrypt, string publicKeyString)
    {
        var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {
                rsa.FromXmlString(publicKeyString.ToString());
                var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                var base64Encrypted = Convert.ToBase64String(encryptedData);
                return base64Encrypted;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }

    public static string Decrypt(string textToDecrypt, string privateKeyString)
    {
        var bytesToDecrypt = Encoding.UTF8.GetBytes(textToDecrypt);

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {                
                rsa.FromXmlString(privateKeyString);

                var resultBytes = Convert.FromBase64String(textToDecrypt);
                var decryptedBytes = rsa.Decrypt(resultBytes, true);
                var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedData.ToString();
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }
}