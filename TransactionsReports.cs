using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class TransactionsReports
    {
        private Transactions[] myTransactions;

        public TransactionsReports(Transactions[] myTransactions)
        {
            this.myTransactions = myTransactions;
        }

        public void SetMyTransactions(Transactions[] myTransactions)
        {
            this.myTransactions = myTransactions;
        }

        public Transactions[] GetMyTransactions()
        {
            return myTransactions;
        }

        public void PrintAllTransactions()
        {
            int lines = File.ReadAllLines("transactions.txt").Length;
            for(int i=0; i<lines; i++)
            {
                Console.WriteLine(myTransactions[i].ToString());
            }
        }
    }
}