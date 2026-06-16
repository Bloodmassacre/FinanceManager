using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private string _password;
        public string Currency = "RUB"; // Валюта
        public DateTime CreatedAt = DateTime.Now;
        public int Balance = 0; 
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}