using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class BooksUtility
    {
        private Books[] myBooks;

        public BooksUtility(Books[] myBooks)
        {
            this.myBooks = myBooks;
        }

        public void SetMyBooks(Books[] myBooks)
        {
            this.myBooks = myBooks;
        }
        
        public Books[] GetMyBooks()
        {
            return myBooks;
        }

        public int SequentialSearch(double searchValue)
        {
            int foundIndex = -1;
            for(int i=0;i<Books.GetCount();i++)
            {
                if(myBooks[i].Equals(searchValue))
                {
                    foundIndex = i;
                }
            }
            return foundIndex;
        }

        public void SortArray()
        {
            for(int i=0;i<Books.GetCount()-1;i++)
            {
                int minIndex = i;
                for(int j=i+1;j<Books.GetCount();j++)
                {
                    if(myBooks[minIndex].CompareTo(myBooks[j])>0)
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
            Books temp = myBooks[x];
            myBooks[x] = myBooks[y];
            myBooks[y] = temp;
        }
    }
}