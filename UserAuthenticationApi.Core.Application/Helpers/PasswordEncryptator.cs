using System.Security.Cryptography;
using System.Text;

namespace UserAuthenticationApi.Core.Application.Helpers
{
    public static class PasswordEncryptator
    {
        public static string HashUserPassword384(string password)
        {
            using(SHA384 sha384 = SHA384.Create())
            {
                byte[] bytes = sha384.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
