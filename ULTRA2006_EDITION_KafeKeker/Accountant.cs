using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ULTRA2006_EDITION_KafeKeker
{
    internal class Accountant : CRUD
    {
        int SelectedIndex;
        int Total_Summ = 0;
        public void Accountant_Menu()
        {
            Console.Clear();

            List<Transaction> transactions = Files_Working.ReadFromFile("Transaction.json");

            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                Меню Бухгалтера");

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
            Console.WriteLine("    ID         Наименование              Сумма             Время записи         Прибавка");
            Console.ResetColor();

            for (int i = 0; i < transactions.Count; i++)
            {
                Console.SetCursorPosition(0, i + 3);
                Console.WriteLine($"    {transactions[i].ID}          {transactions[i].Name}");
                Console.SetCursorPosition(41, i + 3);
                Console.WriteLine(transactions[i].Amount_Of_Money);
                Console.SetCursorPosition(59, i + 3);
                Console.WriteLine(transactions[i].Date);
                Console.SetCursorPosition(80, i + 3);
                Console.WriteLine(transactions[i].Increase);
            }

            if (transactions == null)
            {
                Console.WriteLine("Транзакций нет");
                Console.ReadLine();
                Create();
            }

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 3);
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > transactions.Count - 1 )
                        {
                            SelectedIndex = transactions.Count - 1;
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
                        if (transactions[SelectedIndex].ID != 0)
                        {
                            Delete(SelectedIndex);
                            Accountant_Menu();
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

            string path = "";
            List<Transaction> transactions = Files_Working.ReadFromFile(path);
            int SelectedIndex = 0;

            int id = -1;
            string name = "";
            int amount_of_money = -1;
            DateTime date = new DateTime();
            string increase = "";


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Меню добовления новой транзакции");
            Console.ResetColor();

            Console.SetCursorPosition(2, 1);
            Console.Write("ID транзакции: ");
            Console.SetCursorPosition(2, 2);
            Console.Write("Наименование: ");
            Console.SetCursorPosition(2, 3);
            Console.Write("Сумма: ");
            Console.SetCursorPosition(2, 4);
            Console.Write("Прибавка: ");

            Console.SetCursorPosition(91, 0);
            Console.WriteLine("----------------------------");
            Console.SetCursorPosition(91, 1);
            Console.WriteLine("| S - Сохранить            |");

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 4 )
                        {
                            SelectedIndex = 4;
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
                                Console.SetCursorPosition(17, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    
                                    try
                                    {
                                        Console.SetCursorPosition(17, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        if (transactions != null)
                                        {
                                            foreach (Transaction transaction in transactions)
                                            {
                                                if (transaction.ID == id)
                                                {
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.Write("ID транзакции: Транзакция с таким ID уже существует.");
                                                    Thread.Sleep(1000);
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ResetColor();
                                                    Console.Write("ID транзакции:                                            ");
                                                    Console.SetCursorPosition(17, 1);
                                                    isCorrect = false;
                                                    break;
                                                }
                                                else if (id < 0)
                                                {
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.Write("ID транзакции: ID не может быть меньше нуля.");
                                                    Thread.Sleep(1000);
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ResetColor();
                                                    Console.Write("ID транзакции:                                    ");
                                                    isCorrect = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    isCorrect = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID транзакции: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID транзакции:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(17, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(17, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(17, 2);
                                    name = Console.ReadLine();

                                    foreach (Transaction transaction in transactions)
                                    {
                                        if (transaction.Name == name)
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Наименование: Транзакция с таким наименованием уже существует.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Наименование:                                                    ");
                                            Console.SetCursorPosition(17, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else if (name == "")
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Наименование: Транзакция не может быть с пустым наименованием.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Наименование:                                                   ");
                                            Console.SetCursorPosition(17, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                }

                                Console.SetCursorPosition(17, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(name);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 2);
                                Console.Write(name);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(17, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {

                                        Console.SetCursorPosition(17, 3);
                                        amount_of_money = Convert.ToInt32(Console.ReadLine());

                                        if (amount_of_money == 0)
                                        {
                                            Console.SetCursorPosition(2, 3);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Cумма: Сумма не может быть равна нулю");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 3);
                                            Console.ResetColor();
                                            Console.Write("Сумма:                                 ");
                                            Console.SetCursorPosition(17, 3);
                                            isCorrect = false;
                                            break;
                                        }

                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }

                                    catch(Exception)
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Сумма: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 3);
                                        Console.Write("Сумма:                                      ");
                                    }

                                }

                                Console.SetCursorPosition(17, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(amount_of_money);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 3);
                                Console.Write(amount_of_money);

                                SelectedIndex = 3;
                                break;

                            case 3:
                                Console.SetCursorPosition(17, 4);
                                Console.Write("");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(17, 4);
                                        
                                        foreach (Transaction transaction in transactions)
                                        {
                                            increase = Console.ReadLine();
                                        
                                            if (increase != "+" || increase != "-")
                                            {
                                                Console.SetCursorPosition(2, 4);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Прибавка: Введите значение <-> или <+>");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 4);
                                                Console.ResetColor();
                                                Console.Write("Прибавка:                                           ");
                                                isCorrect = false;
                                                break;
                                            }
                                        }
                                    }

                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Прибавка: Неверные данные");
                                        Thread.Sleep(1000);
                                        Console.SetCursorPosition(2, 4);
                                        Console.ResetColor();
                                        Console.Write("Прибавка:                                           ");
                                        isCorrect = false;
                                    }
                                }

                                Console.SetCursorPosition(17, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(increase);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 4);
                                Console.Write(increase);
                                isCorrect = true;
                                SelectedIndex = 3;

                                break;
                        }
                        break;

                    case ConsoleKey.S:

                        if (id == -1 || amount_of_money == -1 || name == "")
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
                            if (transactions == null)
                            {
                                transactions = new List<Transaction>();
                            }

                            transactions.Add(new Transaction(id, name, amount_of_money, date, increase));
                            Files_Working.SaveToFile(transactions, "transactions.json");

                            Console.SetCursorPosition(2, 5);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Транзакция успешно записана, нажмите <<ENTER>> чтобы продолжить.");
                            Console.ResetColor();
                            Console.ReadLine();
                            Accountant_Menu();
                        }
                        break;
                }
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }
        public void Update(int Index)
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<Transaction> transactions = Files_Working.ReadFromFile("transactions.json");
            int SelectedIndex = 0;

            int id = transactions[Index].ID;
            string name = transactions[Index].Name;
            int amount_of_money = transactions[Index].Amount_Of_Money;
            string increase = transactions[Index].Increase;

            Console.WriteLine("Меню изменения транзакции");
            Console.SetCursorPosition(2, 1);
            Console.Write($"ID транзакции: {id}");
            Console.SetCursorPosition(2, 2);
            Console.Write($"Наименование: {name}");
            Console.SetCursorPosition(2, 3);
            Console.Write($"Сумма: {amount_of_money}");
            Console.SetCursorPosition(2, 4);
            Console.Write($"Прибавка: {increase}");

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
                                        Console.SetCursorPosition(17, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        foreach (Transaction transaction in transactions)
                                        {
                                            if (transaction.ID == id)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID транзакции: Транзакция с таким ID уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID транзакции:                                            ");
                                                Console.SetCursorPosition(17, 1);
                                                isCorrect = false;
                                                break;
                                            }
                                            else if (id < 0)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID транзакции: ID не может быть меньше нуля.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID транзакции:                                    ");
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
                                        Console.Write("ID транзакции: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID транзакции:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(17, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(17, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(17, 2);
                                    name = Console.ReadLine();

                                    foreach (Transaction transaction in transactions)
                                    {
                                        if (transaction.Name == name)
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Наименование: Транзакция с таким наименованием уже существует.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Наименование:                                                    ");
                                            Console.SetCursorPosition(17, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else if (name == "")
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Наименование: Транзакция не может быть с пустым наименованием.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Наименование:                                                   ");
                                            Console.SetCursorPosition(17, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                }

                                Console.SetCursorPosition(17, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(name);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 2);
                                Console.Write(name);

                                SelectedIndex = 2;
                                break;

                            case 2:
                                Console.SetCursorPosition(17, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {

                                        Console.SetCursorPosition(17, 3);
                                        amount_of_money = Convert.ToInt32(Console.ReadLine());

                                        if (amount_of_money == 0)
                                        {
                                            Console.SetCursorPosition(2, 3);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Cумма: Сумма не может быть равна нулю");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 3);
                                            Console.ResetColor();
                                            Console.Write("Сумма:                                 ");
                                            Console.SetCursorPosition(17, 3);
                                            isCorrect = false;
                                            break;
                                        }

                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }

                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Сумма: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 3);
                                        Console.Write("Сумма:                                      ");
                                    }

                                }

                                Console.SetCursorPosition(17, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(amount_of_money);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 3);
                                Console.Write(amount_of_money);

                                SelectedIndex = 3;
                                break;

                            case 3:
                                Console.SetCursorPosition(17, 4);
                                Console.Write("");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(17, 4);

                                        ConsoleKeyInfo info = Console.ReadKey();

                                        

                                        foreach (Transaction transaction in transactions)
                                        {

                                            if (increase == "+")
                                            {
                                                Total_Summ += amount_of_money;
                                                isCorrect = true;
                                            }

                                            else if (increase == "-")
                                            {
                                                Total_Summ -= amount_of_money;
                                                isCorrect = true;
                                            }

                                            else if (info.Key != ConsoleKey.Add || info.Key != ConsoleKey.Subtract)
                                            {
                                                Console.SetCursorPosition(2, 4);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.Write("Прибавка: Введите значение <-> или <+>");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 4);
                                                Console.ResetColor();
                                                Console.Write("Прибавка:                                           ");
                                                isCorrect = false;
                                                break;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Прибавка: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Прибавка:                                      ");
                                    }
                                }

                                Console.SetCursorPosition(17, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(increase);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 4);
                                Console.Write(increase);
                                break;
                        }
                        break;

                    case ConsoleKey.S:

                        transactions[Index] = new Transaction(id, name, amount_of_money, DateTime.Now, increase);
                        Files_Working.SaveToFile(transactions, "transactions.json");

                        Console.SetCursorPosition(2, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Транзакция успешно изменена. Нажмите <<ENTER>> чтобы продолжить");
                        Console.ResetColor();
                        Console.ReadLine();
                        Accountant_Menu();
                        break;

                }
            }
        }
        public void Delete(int Index)
        {
            List<Transaction> transactions = Files_Working.ReadFromFile("transactions.json");
            transactions.RemoveAt(Index);
            Files_Working.SaveToFile(transactions, "transactions.json");
        }
    }
}
