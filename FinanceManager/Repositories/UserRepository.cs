using FinanceManager.Data;
using FinanceManager.Models;
using FinanceManager.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly Database _db = new Database();
        public UserRepository()
        {
            
        }
        public User Login(string username, string password)
        {
            var hash = PasswordHasher.Hash(password);
            var existingUser = _db.Users.FirstOrDefault(s => s.Name == username);
            if (existingUser != null)
            {
                return _db.Users.FirstOrDefault(u => u.Password == hash);
            }
            throw new Exception("Неверное имя или пароль!");
        }
        public User Register(string username, string password, string email)
        {
            var existingUser = _db.Users.FirstOrDefault(s => s.Name == username && s.Email == email);
            if (existingUser != null)
            {
                throw new Exception("Такой пользователь уже существует!");
            }
            var user = new User()
            {
                Name = username,
                Email = email,
                Password = PasswordHasher.Hash(password)
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
    }
}
