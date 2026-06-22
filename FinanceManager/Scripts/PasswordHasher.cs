using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Scripts
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return HashedPassword;
        }
        public static bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
