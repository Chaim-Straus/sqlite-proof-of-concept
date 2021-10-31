using System.Security.Cryptography;
using System.Text;

namespace yoc_csharp_banking
{
    public class encryptSHA256
    {
        public static string encrypt(string rawData)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
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

