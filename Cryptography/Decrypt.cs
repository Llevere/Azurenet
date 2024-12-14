using System.Security.Cryptography;
using System.Text;

namespace Azurenet.Cryptography
{
    public class DecryptHelper
    {
        private static readonly string EncryptionKey = "test"; // Replace with a secure key
        public static string Decrypt(string encryptedText)
        {
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.Key = key;
            aes.IV = new byte[16]; // Initialization vector
            using var decryptor = aes.CreateDecryptor();
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
