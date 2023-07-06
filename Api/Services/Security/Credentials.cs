using System.Security.Cryptography;
using System.Text;

namespace Api.Services.Security
{
    public static class Credentials
    {
        private const int keySize = 64;
        private const int iterations = 350000;

        public static byte[] Hash(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
            iterations,
                HashAlgorithmName.SHA512,
                keySize);

            return hash;
        }

        public static bool Verify(string password, byte[] hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }

        public static string CreateVerificationCode() =>
            Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
    }
}
