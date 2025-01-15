using System.Security.Cryptography;
using System.Text;

namespace PassswortManagerAPI.Helpers
{
    public class Hashing
    {
        public static string CreateSalt(int size)
        {
            // Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string password, string salt)
        {
            string saltedPassword = string.Concat(password, salt);

            // Use a hashing algorithm (e.g., SHA-256) to compute the hash.
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
