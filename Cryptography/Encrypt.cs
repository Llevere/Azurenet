using System.Security.Cryptography;
using System.Text;

namespace Azurenet.Cryptography
{
    public class EncryptionHelper
    {
        private static readonly string EncryptionKey = "test"; // Replace with a secure key

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.Key = key;
            aes.IV = new byte[16]; // Initialization vector
            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

    }
}