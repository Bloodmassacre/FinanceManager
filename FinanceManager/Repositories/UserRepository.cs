using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository()
        {
            
        }
        public User Login(string username, string password)
        {
            var ExistingUser = new User();
            var user = new User()
            {

            };
            return user;
        }
        public User Register(string username, string password, string email)
        {
            var user = new User()
            {

            };
            return user;
        }
    }
}
