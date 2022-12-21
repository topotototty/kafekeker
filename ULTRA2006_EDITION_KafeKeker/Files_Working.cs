using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    static class Files_Working
    {
        static public void SaveToFile<T>(T list, string path)
        {
            if (!File.Exists(path))
            {
                FileStream fileStream = File.Create(path);
                fileStream.Dispose();
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, json);
        }

        static public List<User> ReadUsersFromFile()
        {
            List<User> users;
            if (!File.Exists("Users.json"))
            {
                FileStream fileStream = File.Create("Users.json");
                fileStream.Dispose();

                users = new List<User>();
                users.Add(new User(0, 0, "Admin", "Admin"));
                SaveToFile(users, "Users.json");
            }
            else
            {
                string usersInfo = File.ReadAllText("Users.json");
                users = JsonConvert.DeserializeObject<List<User>>(usersInfo);
            }
            return users;
        }

        static public List<Transaction> ReadFromFile(string path)
        {
            path = "transaction.json";

            List<Transaction> transactions;

            transactions = new List<Transaction>();
            transactions.Add(new Transaction(0, "fhgbdhrg", 12345, DateTime.Now, true));
            SaveToFile(transactions, "transaction.json");

            if (!File.Exists(path))
            {
                FileStream fileStream = File.Create(path);
                fileStream.Dispose();

            }
            string resultInfo = File.ReadAllText(path);
            transactions = JsonConvert.DeserializeObject<List<Transaction>>(resultInfo);

            return transactions;
        }
    }
}
