using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    public class Admin : CRUD
    {
        int SelectedIndex;

        public void Admin_Menu()
        {
            Console.Clear();

            List<User> users = Files_Working.ReadUsersFromFile();
            string[] options = new string[users.Count];

            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                Меню Администратора");

            Console.SetCursorPosition(100, 2);
            Console.WriteLine("| F1 - Создание    |");
            Console.SetCursorPosition(100, 3);
            Console.WriteLine("| F2 - Изменение   |");
            Console.SetCursorPosition(100, 4);
            Console.WriteLine("| Del - Удаление   |");
            Console.SetCursorPosition(100, 5);
            Console.WriteLine("| Esc - Вернутся   |");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("____________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    ID         Логин                 Роль                      Пароль");
            Console.ResetColor();

            for (int i = 0; i < options.Length; i++)
            {
                string role = "";
                options[i] = users[i].ID + "  " + users[i].Login;

                Console.SetCursorPosition(0, i + 3);
                Console.WriteLine("    " + users[i].ID + "          " + users[i].Login);
                switch (users[i].Role)
                {
                    case (int)Role.Admin:
                        role = "  Администратор";
                        break;
                    case (int)Role.HR:
                        role = "  Mенеджер персонала";
                        break;
                    case (int)Role.Accountant:
                        role = "  Бугалтер";
                        break;
                    case (int)Role.Warehouse_Manager:
                        role = "  Cклад-менеджер";
                        break;
                    case (int)Role.Cashier:
                        role = "  Кассир";
                        break;
                }

                Console.SetCursorPosition(35, i + 3);
                Console.WriteLine(role);
                Console.SetCursorPosition(63, i + 3);
                Console.WriteLine(users[i].Password);
            }

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 3);
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > users.Count - 1)
                        {
                            SelectedIndex = users.Count - 1;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        break;

                    case ConsoleKey.F1:
                        Create();
                        break;

                    case ConsoleKey.F2:
                       Update(SelectedIndex);
                        break;

                    case ConsoleKey.Delete:
                        if (users[SelectedIndex].ID != 0)
                        {
                            Delete(SelectedIndex);
                            Admin_Menu();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        Read();
                        break;

                    case ConsoleKey.Escape:
                        //выход авторизации
                        break;
                }
            }
        }

        public void Create()
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<User> users = Files_Working.ReadUsersFromFile();
            int SelectedIndex = 0;

            int id = -1;
            string login = "";
            string password = "";
            int role = -1;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Меню добовления нового пользователя");
            Console.ResetColor();

            Console.SetCursorPosition(2, 1);
            Console.Write("ID пользователя: ");
            Console.SetCursorPosition(2, 2);
            Console.Write("Логин пользователя: ");
            Console.SetCursorPosition(2, 3);
            Console.Write("Пароль пользователя: ");
            Console.SetCursorPosition(2, 4);
            Console.Write("Роль пользователя: ");

            Console.SetCursorPosition(91, 0);
            Console.WriteLine("-------------Роли-----------");
            Console.SetCursorPosition(91, 1);
            Console.WriteLine("| 0 - Админ                |");
            Console.SetCursorPosition(91, 2);
            Console.WriteLine("| 1 - Менеджер персонала   |");
            Console.SetCursorPosition(91, 3);
            Console.WriteLine("| 2 - Менеджер склада      |");
            Console.SetCursorPosition(91, 4);
            Console.WriteLine("| 3 - Бухгалтер            |");
            Console.SetCursorPosition(91, 5);
            Console.WriteLine("| 4 - Кассир               |");
            Console.SetCursorPosition(91, 6);
            Console.WriteLine("| S - Сохранить            |");

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 3)
                        {
                            SelectedIndex = 3;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        switch (SelectedIndex)
                        {
                            case 0:
                                Console.SetCursorPosition(19, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(19, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (user.ID == id)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Ошибка. Пользователь с таким ID уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID пользователя:                                                ");
                                                Console.SetCursorPosition(19, 1);
                                                isCorrect = false;
                                                break;
                                            }
                                            else if (id < 0)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Ошибка. ID не может быть меньше нуля.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID пользователя:                                                ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID пользователя: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(19, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(19, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(22, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(22, 2);
                                    login = Console.ReadLine();

                                    foreach (User user in users)
                                    {
                                        if (user.Login == login)
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь с таким логином уже существует.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else if (login == "")
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь не может быть с пустым логином.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                }

                                Console.SetCursorPosition(22, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(login);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(22, 2);
                                Console.Write(login);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(23, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(23, 3);
                                    password = Console.ReadLine();


                                    if (password == "")
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Пароль пользователя: Пользователь не может быть с пустым паролем.");
                                        Thread.Sleep(1000);
                                        Console.SetCursorPosition(2, 3);
                                        Console.ResetColor();
                                        Console.Write("Пароль пользователя:                                                                         ");
                                        Console.SetCursorPosition(23, 3);
                                        isCorrect = false;
                                        break;
                                    }
                                    else
                                    {
                                        isCorrect = true;
                                    }
                                }

                                Console.SetCursorPosition(23, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(password);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(23, 3);
                                Console.Write(password);

                                SelectedIndex = 3;
                                break;

                            case 3:

                                Console.SetCursorPosition(21, 4);
                                Console.Write("                 ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(21, 4);
                                        role = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (role < 0 || role > 4)
                                            {
                                                Console.SetCursorPosition(2, 4);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Роль пользователя: Такой роли не существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 4);
                                                Console.ResetColor();
                                                Console.Write("Роль пользователя:                                                ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Роль пользователя: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Роль пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(21, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(role);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(21, 4);
                                Console.Write(role);
                                break;
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.S:

                        if (id == -1 || role == -1 || login == "" || password == "")
                        {
                            Console.SetCursorPosition(2, 5);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Поля не заполнены.");
                            Thread.Sleep(1000);
                            Console.ResetColor();
                            Console.SetCursorPosition(2, 5);
                            Console.Write("                                                    ");
                        }
                        else
                        {
                            users.Add(new User(id, role, login, password));
                            Files_Working.SaveToFile(users, "Users.json");

                            Console.SetCursorPosition(2, 5);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ползователь успешно создан, нажмите <<ENTER>> чтобы продолжить.");
                            Console.ResetColor();
                            Console.ReadLine();
                            Create();
                        }
                        break;
                    case ConsoleKey.Escape:
                        Admin_Menu();
                        break;
                }
            }
        }

        public void Read()
        {
            Console.CursorVisible = true;

            Console.Clear();

            List<User> users = Files_Working.ReadUsersFromFile();
            List<User> FoundUsers = null;

            Console.WriteLine("Меню поиска. ");

            Console.Write("Поиск: ");
            string search = Console.ReadLine();

            FoundUsers = users.FindAll(x => x.ID == 1);
            FoundUsers = users.FindAll(x => x.Login.Contains(search));

            if (FoundUsers != null)
            {
                int i = 0;
                foreach (User user in FoundUsers)
                {
                    string role = "";
                    i++;

                    Console.SetCursorPosition(0, i + 1);

                    switch (user.Role)
                    {
                        case (int)Role.Admin:
                            role = "Администратор";
                            break;
                        case (int)Role.HR:
                            role = "Mенеджер персонала";
                            break;
                        case (int)Role.Accountant:
                            role = "Бугалтер";
                            break;
                        case (int)Role.Warehouse_Manager:
                            role = "Cклад-менеджер";
                            break;
                        case (int)Role.Cashier:
                            role = "Кассир";
                            break;
                    }
                    Console.WriteLine(user.ID + "  " + user.Login + " Роль: " + role);
                    Console.ResetColor();

                    Console.WriteLine("Нажмите любую клавишу для выхода.");
                    Console.ReadKey(true);
                    Admin_Menu();
                }
            }
            else
            {
                Console.WriteLine("Ничего не найдено. Нажмите любую клавишу для выхода.");
                Console.ReadKey(true);
                Admin_Menu();
            }
        }

        public void Update(int Index)
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<User> users = Files_Working.ReadUsersFromFile();
            int SelectedIndex = 0;

            int id = users[Index].ID;
            string login = users[Index].Login;
            string password = users[Index].Password;
            int role = users[Index].Role;

            Console.WriteLine("Меню изменения пользователя");
            Console.SetCursorPosition(2, 1);
            Console.Write($"ID пользователя: {id}");
            Console.SetCursorPosition(2, 2);
            Console.Write($"Логин пользователя: {login}");
            Console.SetCursorPosition(2, 3);
            Console.Write($"Пароль пользователя: {password}");
            Console.SetCursorPosition(2, 4);
            Console.Write($"Роль пользователя: {role}");

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 3)
                        {
                            SelectedIndex = 3;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        switch (SelectedIndex)
                        {
                            case 0:
                                Console.SetCursorPosition(19, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(19, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (user.ID == id && users[Index].ID != id)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Пользователь с таким ID уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write($"ID пользователя:                                                ");
                                                Console.SetCursorPosition(19, 1);
                                                isCorrect = false;
                                                break;
                                            }
                                            else if (id < 0)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: ID не может быть меньше нуля.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID пользователя:                                                  ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID пользователя: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(19, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(19, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(22, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(22, 2);
                                    login = Console.ReadLine();

                                    foreach (User user in users)
                                    {
                                        if (user.Login == login && users[Index].Login != login)
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь с таким логином уже существует.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else if (login == "")
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь не может быть с пустым логином.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                }

                                Console.SetCursorPosition(22, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(login);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(22, 2);
                                Console.Write(login);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(23, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(23, 3);
                                    password = Console.ReadLine();


                                    if (password == "")
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Пароль пользователя: Ошибка. Пользователь не может быть с пустым паролем.");
                                        Thread.Sleep(1000);
                                        Console.SetCursorPosition(2, 3);
                                        Console.ResetColor();
                                        Console.Write("Пароль пользователя:                                                                         ");
                                        Console.SetCursorPosition(23, 3);
                                        isCorrect = false;
                                        break;
                                    }
                                    else
                                    {
                                        isCorrect = true;
                                    }
                                }

                                Console.SetCursorPosition(23, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(password);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(23, 3);
                                Console.Write(password);

                                SelectedIndex = 3;
                                break;

                            case 3:

                                Console.SetCursorPosition(21, 4);
                                Console.Write("                 ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(21, 4);
                                        role = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (role < 0 || role > 5)
                                            {
                                                Console.SetCursorPosition(2, 4);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Роль пользователя: Ошибка. Такой роли не существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 4);
                                                Console.ResetColor();
                                                Console.Write("Роль пользователя:                                                ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Роль пользователя: Ошибка. Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Роль пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(21, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(role);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(21, 4);
                                Console.Write(role);
                                break;
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.S:

                        users[Index] = new User(id, role, login, password);
                        Files_Working.SaveToFile(users, "Users.json");

                        Console.SetCursorPosition(2, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Ползователь успешно изменен.");
                        Console.ResetColor();
                        Console.ReadLine();
                        Create();
                        break;

                    case ConsoleKey.Escape:
                        Admin_Menu();
                        break;
                }
            }
        }

        public void Delete(int Index)
        {
            List<User> users = Files_Working.ReadUsersFromFile();
            users.RemoveAt(Index);
            Files_Working.SaveToFile(users, "Users.json");
        }
    }
}

