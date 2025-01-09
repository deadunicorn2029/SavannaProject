using System.Security.Cryptography;

namespace Savanna.Services.Helpers
{
    public class Hasher
    {
        /// <summary>
        /// Generates a hashed representation of the provided input string using PBKDF2 with SHA256.
        /// A random 16-byte salt is generated and included in the hash.
        /// </summary>
        /// <param name="value"> The input string to be hashed. </param>
        /// <returns> A Base64-encoded string containing the salt and hash combined. </returns>
        public static string Hash(string value)
        {
            byte[] salt = new byte[16]; //should to be random and stored in DB
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(value, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);

                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifies if a given input string matches a stored hash using PBKDF2 with SHA256.
        /// </summary>
        /// <param name="enteredValue"> The input string to verify (e.g., a password). </param>
        /// <param name="storedHash"> The stored Base64-encoded hash (containing salt and hash). </param>
        /// <returns> True if the input matches the stored hash; otherwise, false. </returns>
        public static bool Verify(string enteredValue, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredValue, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                for (int i = 0; i<32; i++)
                {
                    if (hashBytes[i+16] != hash[i])
                        return false;
                }
            }
            return true;
        }
    }
}
