using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class TransactionsUtility
    {
        private Transactions[] myTransactions;

        public TransactionsUtility(Transactions[] myTransactions)
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

        public int SequentialSearch(double searchValue)
        {
            int foundIndex = -1;
            for(int i=0;i<Transactions.GetCount();i++)
            {
                if(myTransactions[i].Equals(searchValue))
                {
                    foundIndex = i;
                }
            }
            return foundIndex;
        }

        public void SortArray()
        {
            for(int i=0;i<Transactions.GetCount()-1;i++)
            {
                int minIndex = i;
                for(int j=i+1;j<Transactions.GetCount();j++)
                {
                    if(myTransactions[minIndex].CompareTo(myTransactions[j]) > 0)
                    {
                        minIndex = j;
                    }
                }
                if(minIndex != i)
                {
                    Swap(i, minIndex);
                }
            }
        }
        
        public void Swap(int x, int y)
        {
            Transactions temp = myTransactions[x];
            myTransactions[x] = myTransactions[y];
            myTransactions[y] = temp;
        }
    }
}