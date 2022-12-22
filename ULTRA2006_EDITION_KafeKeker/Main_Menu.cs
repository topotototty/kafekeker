using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    internal class Main_Menu
    {
        static int selection = 0;
        public string login;
        public string password;
        List<string> PasswordChars = new List<string>();
        public void Menu()
        {
            Console.SetCursorPosition(45, 0);
            Console.WriteLine("Добро пожаловать в *KAFE KEKER*");
            Console.WriteLine("________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(0, 6);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Для начала ввода нажмите <<ENTER>>");
            Console.ResetColor();

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: "); 
            Console.WriteLine("  Авторизоваться");

            while (true)
            {
                Arrows.DisplayArrow(selection, 2);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        selection++;

                        if (selection > 2)
                        {
                            selection = 2;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (selection > 0)
                        {
                            selection--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        if (selection == 0)
                        {
                            Console.SetCursorPosition(9, 2);
                            login = Console.ReadLine();
                        }

                        if (selection == 1)
                        {
                            Console.SetCursorPosition(10, 3);

                            PasswordChars = new List<string>();
                            Console.SetCursorPosition(14, 3);
                            Console.WriteLine("                            ");
                            Console.SetCursorPosition(14, 3);
                            while (true)
                            {
                                ConsoleKeyInfo key = Console.ReadKey(true);
                                if (key.Key == ConsoleKey.Enter) break;

                                if (key.Key == ConsoleKey.Backspace)
                                {
                                    if (PasswordChars.Count > 0)
                                    {
                                        PasswordChars.RemoveAt(PasswordChars.Count - 1);
                                        Console.Write("\b \b");
                                    }
                                }
                                else if (key.KeyChar != '\u0000')
                                {
                                    PasswordChars.Add(key.KeyChar.ToString());
                                    Console.Write("*");
                                }
                            }
                            password = string.Join("", PasswordChars);
                        }

                        if (selection == 2)
                        {
                            Authorization.Check(login, password);

                            if (login == null || password == null)
                            {
                                Console.WriteLine("Введите данные");
                            }
                        }
                        break;
                }
            };
        }
    }
}
