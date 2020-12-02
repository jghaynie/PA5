using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class Transactions
    {
        private double rentalId;
        private double isbn;
        private string customerName;
        private string customerEmail;
        private string rentalDate;
        private string returnDate;
        private static int count;

        public Transactions() {}

        public Transactions(double rentalId, double isbn, string customerName, string customerEmail, string rentalDate, string returnDate)
        {
            this.rentalId = rentalId;
            this.isbn = isbn;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.rentalDate = rentalDate;
            this.returnDate = returnDate;
        }

        public void SetRentalId(double rentalId)
        {
            this.rentalId = rentalId;
        }

        public double GetRentalId()
        {
            return rentalId;
        }

        public void SetIsbn(double isbn)
        {
            this.isbn = isbn;
        }

        public double GetIsbn()
        {
            return isbn;
        }

        public void SetCustomerName(string customerName)
        {
            this.customerName = customerName;
        }

        public string GetCustomerName()
        {
            return customerName;
        }

        public void SetCustomerEmail(string customerEmail)
        {
            this.customerEmail = customerEmail;
        }

        public string GetCustomerEmail()
        {
            return customerEmail;
        }

        public void SetRentalDate(string rentalDate)
        {
            this.rentalDate = rentalDate;
        }

        public string GetRentalDate()
        {
            return rentalDate;
        }

        public void SetReturnDate(string returnDate)
        {
            this.returnDate = returnDate;
        }

        public string GetReturnDate()
        {
            return returnDate;
        }

        public override string ToString()
        {
            return($"Rental ID: {rentalId}, ISBN: {isbn}, Customer Name: {customerName}, Customer Email: {customerEmail}, Rental Date: {rentalDate} Return Date: {returnDate}");
        }

        public static void SetCount(int value)
        {
            value = count;
        }

        public static int GetCount()
        {
            return count;
        }

        public static void IncCount()
        {
            count++;
        }

        public static void DecCount()
        {
            count--;
        }

        public bool Equals(Transactions tempTransactions)
        {
            return this.isbn == tempTransactions.GetIsbn();
        }

        public bool Equals(double tempIsbn)
        {
            return this.isbn == tempIsbn;
        }

        public int CompareTo(Transactions value)
        {
            return(this.isbn.CompareTo(value.GetIsbn()));
        }

        public int CompareToEmail(Transactions value)
        {
            return(this.customerEmail.CompareTo(value.GetCustomerEmail()));
        }

        public int CompareToDate(Transactions value)
        {
            return(this.rentalDate.CompareTo(value.GetRentalDate()));
        }
    }
}