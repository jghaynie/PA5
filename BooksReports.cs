using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class BooksReports
    {
        private Books[] myBooks;

        public BooksReports(Books[] myBooks)
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

        public void PrintAllBooks()
        {
            int lines = File.ReadAllLines("books.txt").Length;
            for(int i=0; i<lines; i++)
            {
                Console.WriteLine(myBooks[i].ToString());
            }
        }
    }
}