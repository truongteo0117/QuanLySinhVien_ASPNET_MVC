using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public class EncryptionHelper
    {
        public string DecryptString(string encryptedString, string secretKey)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encryptedString);

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(buffer))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        public string EncryptString(string encryptedString, string secretKey)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(encryptedString);
            using (var aes = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(secretKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64,
            0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(textBytes, 0, textBytes.Length);
                        cs.Close();
                    }
                    encryptedString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptedString;
        }
    }
}
