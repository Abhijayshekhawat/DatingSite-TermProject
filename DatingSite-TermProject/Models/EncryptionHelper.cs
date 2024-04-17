using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace DatingSite_TermProject.Models
{
    public class EncryptionHelper
    {
        private const string EncryptionKey = "rwrryryy4484847458thjrthrr"; // Change this key to your own secure key
        private const string SaltValue = "Abhijay&JohnsonMatchup2024"; // Change this salt value to your own secure value

        public static string Encrypt(string plainText)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(SaltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, saltBytes, 1000);
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(SaltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, saltBytes, 1000);
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    encryptedText = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return encryptedText;
        }
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
