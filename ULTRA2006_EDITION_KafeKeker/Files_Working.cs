using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ULTRA2006_EDITION_KafeKeker
{
    static class Files_Working
    {
        static public void SaveToFile<T>(T list, string path)
        {
            if (!File.Exists(Environment.SystemDirectory + "/" + path))
            { 
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + path);
                fileStream.Dispose();
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(Environment.SystemDirectory + "/" + path, json);
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
            path = "transactions.json";
            List<Transaction> transactions;

            if (!File.Exists(Environment.SystemDirectory + "/" + path))
            {
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + path);
                fileStream.Dispose();

                transactions = new List<Transaction>();
                transactions.Add(new Transaction(0, "0", 0, DateTime.Now, "+"));
                Files_Working.SaveToFile(transactions, "transactions.json");
            }

            string resultInfo = File.ReadAllText(Environment.SystemDirectory + "/" + path);
            transactions = JsonConvert.DeserializeObject<List<Transaction>>(resultInfo);

            return transactions;
        }
    }
}
