using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    internal class Transaction
    {
        public int ID;
        public int Amount_Of_Money;
        public string Name;
        public DateTime Date;
        public string Increase;

        public Transaction(int id, string name, int amount_of_money, DateTime date, string increase)
        {
            ID = id;
            Name = name;
            Amount_Of_Money = amount_of_money;
            Date = date;
            Increase = increase;
        }
    }
}
