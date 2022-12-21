using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    static class Authorization
    {
        public static bool Check(string login, string password)
        {
            bool isAuthorized = false;

            List<User> users = Files_Working.ReadUsersFromFile();

            foreach (User user in users)
            {
                if (user.Login != login || user.Password != password)
                {
                    Console.WriteLine("  Такого пользователя не существует");
                    Thread.Sleep(2000);

                    Console.Clear();
                    Main_Menu menu = new Main_Menu();
                    menu.Menu();
                }

                else
                {
                    switch (user.Role)
                    {
                        case (int)Role.Admin:
                            Admin admin = new Admin();
                            admin.Admin_Menu();
                            break;

                        case (int)Role.HR:
                            break;

                        case (int)Role.Warehouse_Manager:
                                                   
                            break;

                        case (int)Role.Accountant:

                            break;

                        case (int)Role.Cashier:
                            break;
                    }
                    isAuthorized = true;
                }
            }
            return isAuthorized;
        }
    }
}
