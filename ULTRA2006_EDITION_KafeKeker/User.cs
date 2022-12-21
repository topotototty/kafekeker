using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    internal class User
    {
        public int ID;
        public string Login;
        public string Password;
        public string Name;
        public string Surname;
        public string Patronymic;
        public int Role;

        public User(int id, int role, string login, string password, string name = null, string surname = null, string patronymic = null)
        {
            ID = id;
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Role = role;
        }
    }
}
