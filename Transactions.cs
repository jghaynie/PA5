using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class Transactions
    {

        // initializing variables, as double's, string's and a static int for the count
        private double rentalId;
        private string isbn;
        private string customerName;
        private string customerEmail;
        private string rentalDate;
        private string returnDate;
        private static int count;

        public Transactions() {}

        public Transactions(double rentalId, string isbn, string customerName, string customerEmail, string rentalDate, string returnDate)
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

        // GETTERS AND SETTERS
        public double GetRentalId()
        {
            return rentalId;
        }

        public void SetIsbn(string isbn)
        {
            this.isbn = isbn;
        }

        public string GetIsbn()
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

        // converts the returndate string to a datetime to format it proeprly, and then back again. 
        public string GetRentalDate()
        {
            return Convert.ToDateTime(rentalDate).ToString("d");
        }

        public void SetReturnDate(string returnDate)
        {
            this.returnDate = returnDate;
        }

        // converts the returndate string to a datetime to format it proeprly, and then back again. 
        public string GetReturnDate()
        {
            if (returnDate == "0/0/0000")
            {
                return returnDate;
            }
            return Convert.ToDateTime(returnDate).ToString("d");
        }

        // an override method to display the entire book value when prompted with both a myBook array instance, and an index variable

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

        // used to increase or decrease book count in order to maintain functionality of the project
        // as well as make the GetAlLBooks() function work, and the initial count of the books files
        public static void IncCount()
        {
            count++;
        }

        // this is a function to decrease the book count
        // it was never used but I included it anyuways
        // so I can toy around with it after this project is graded
        public static void DecCount()
        {
            count--;
        }

        // these two equals methods test whether or not the tempIsbn submitted to the Sequential Search method 
        // in TransactionsUtility is equal to whatever value it is being compared to. I set it to the ISBN, as that
        // is the only consistent variable I have amongst both Books.TXT and Transactions.TXT

        public bool Equals(Transactions tempTransactions)
        {
            return this.isbn == tempTransactions.GetIsbn();
        }

        // the override method to accept the string input

        public bool Equals(string tempIsbn)
        {
            return this.isbn == tempIsbn;
        }

        // this is the only compare to method used, and it is only used in the Swap and Sort methods
        // I included some others that I used throughout testing but didn't continue using in the
        // final program, just for my later reference
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