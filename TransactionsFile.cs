using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace pleasework
{
    public class TransactionsFile
    {
        private string fileName;

        public TransactionsFile(string fileName)
        {
            this.fileName = fileName;
        }

        public void SetFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName()
        {
            return fileName;
        }

        public Transactions[] GetAllTransactions()
        {
            Transactions[] myTransactions = new Transactions[200];
            Transactions.SetCount(0);
            StreamReader inFile = new StreamReader(fileName);
            string[] input = File.ReadAllLines(fileName);
            foreach (string line in input)
            {
                string[] transactionInfo = line.Split('#');
                myTransactions[Transactions.GetCount()] = new Transactions(double.Parse(transactionInfo[0]), double.Parse(transactionInfo[1]), transactionInfo[2], transactionInfo[3], transactionInfo[4], transactionInfo[5]);
                Transactions.IncCount();
            }
            inFile.Close();
            return myTransactions;
        }
        
        public Transactions[] RentBook()
        {
            TransactionsFile transactionsFile = new TransactionsFile("transactions.txt");
            Transactions[] myTransactions = transactionsFile.GetAllTransactions();
            TransactionsReports transactionsReports = new TransactionsReports(myTransactions);
            TransactionsUtility transactionsUtility = new TransactionsUtility(myTransactions);

            BooksFile booksFile = new BooksFile("books.txt");
            Books[] myBooks = booksFile.GetAllBooks();
            BooksReports booksReports = new BooksReports(myBooks);
            BooksUtility booksUtility = new BooksUtility(myBooks);

            string customerNameInput = "";
            string customerEmailInput = "";
            string isbnInput = "";
            string confirmInput = "";
            string defaultReturnDate = "0/0/0000";
            double isbnDoubleInput = 0;
            int searchIndex = 0;
            int transactionLength = File.ReadAllLines("transactions.txt").Length;
            DateTime currentDate = DateTime.Today;

            Console.WriteLine();
            booksReports.PrintAllBooks();
            Console.WriteLine();

            Console.WriteLine("Select an ISBN to Rent:");
            Console.WriteLine("'Cancel' to Cancel");
            do {
                Console.Write("-- ");
                isbnInput = Console.ReadLine();
                if (isbnInput.ToUpper() == "CANCEL")
                {
                    return myTransactions;
                }
                else if (isbnInput.Length > 10)
                {
                    Console.WriteLine($"Error: {isbnInput} is an Invalid ISBN, Try Again");
                }
                else if (isbnInput.Length < 10)
                {
                    Console.WriteLine($"Error: {isbnInput} is an Invalid ISBN, Try Again");
                }
                else if (isbnInput.Length == 10)
                {
                    isbnDoubleInput = Convert.ToDouble(isbnInput);
                    searchIndex = booksUtility.SequentialSearch(isbnDoubleInput);
                    if (searchIndex == -1)
                    {
                        Console.WriteLine("Error: Book does not exist");
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
            } while (isbnInput.ToUpper() != "CANCEL" && isbnInput.Length != 10);

            double rentalId = transactionLength+1;

            Console.WriteLine("Checking out:");
            Console.WriteLine($"Rental ID: {rentalId:0000}");
            Console.WriteLine($"Title: {myBooks[searchIndex].GetTitle()}");
            Console.WriteLine();

            Console.WriteLine("Enter Customer Name:");
            Console.WriteLine("'Cancel' to Cancel");
            Console.Write("-- ");
            customerNameInput = Console.ReadLine();
            
            if (customerNameInput.ToUpper() == "CANCEL")
            {
                return myTransactions;
            }

            Console.Clear();
            Console.WriteLine("Checking out:");
            Console.WriteLine($"Rental ID: {rentalId:0000}");
            Console.WriteLine($"Title: {myBooks[searchIndex].GetTitle()}");
            Console.WriteLine($"Customer Name: {customerNameInput}");
            Console.WriteLine();

            Console.WriteLine("Enter Customer Email:");
            Console.WriteLine("'Cancel' to Cancel");
            Console.Write("-- ");
            customerEmailInput = Console.ReadLine();
            
            if (customerEmailInput.ToUpper() == "CANCEL")
            {
                return myTransactions;
            }

            Console.Clear();
            Console.WriteLine("Checking out:");
            Console.WriteLine($"Rental ID: {rentalId:0000}");
            Console.WriteLine($"Title: {myBooks[searchIndex].GetTitle()}");
            Console.WriteLine($"Customer Name: {customerNameInput}");
            Console.WriteLine($"Customer Email: {customerEmailInput}");
            Console.WriteLine($"Rental Date: {currentDate:d}");
            Console.WriteLine($"Return Date: {defaultReturnDate}");
            Console.WriteLine();

            Console.WriteLine("'Confirm' to Change");
            Console.WriteLine("'Cancel' to Cancel Change");
            Console.WriteLine("Enter your selection:");

            do {

                Console.Write("-- ");
                confirmInput = Console.ReadLine();

                switch(confirmInput.ToUpper())
                {
                    case "CONFIRM":
                        int bookFileLength = File.ReadAllLines("books.txt").Length;
                        string[] tempBooks = File.ReadAllLines("books.txt");
                        myBooks[searchIndex].SetCopies(Convert.ToDouble(myBooks[searchIndex].GetCopies())-1);
                        tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                        File.Create("books.txt").Close();
                        using (StreamWriter sw = File.AppendText("books.txt"))
                        {
                            for (int i = 0; i!= bookFileLength; i++)
                            {
                                sw.WriteLine(tempBooks[i]);
                            }
                        }
                        using (StreamWriter sw = File.AppendText("transactions.txt"))
                        {
                            sw.WriteLine($"{rentalId:0000}#{isbnInput}#{customerNameInput}#{customerEmailInput}#{currentDate:d}#{defaultReturnDate}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Transaction has been added.");
                        myBooks = booksFile.GetAllBooks();
                        myTransactions = transactionsFile.GetAllTransactions();
                        return myTransactions;
                    case "CANCEL":
                        break;
                    default:
                        Console.WriteLine("Error: Invalid Selection, Try Again");
                        break;
                }

            } while (confirmInput.ToUpper() != "CANCEL" && confirmInput.ToUpper() != "CONFIRM");

            return myTransactions;
        }

        // return($"Rental ID: {rentalId}, ISBN: {isbn}, Customer Name: {customerName}, Customer Email: {customerEmail}, Rental Date: {rentalDate} Return Date: {returnDate}");

        public Transactions[] ReturnBook()
        {
            TransactionsFile transactionsFile = new TransactionsFile("transactions.txt");
            Transactions[] myTransactions = transactionsFile.GetAllTransactions();
            TransactionsReports transactionsReports = new TransactionsReports(myTransactions);
            TransactionsUtility transactionsUtility = new TransactionsUtility(myTransactions);

            BooksFile booksFile = new BooksFile("books.txt");
            Books[] myBooks = booksFile.GetAllBooks();
            BooksReports booksReports = new BooksReports(myBooks);
            BooksUtility booksUtility = new BooksUtility(myBooks);

            string customerEmailInput = "";
            string isbnInput = "";
            string confirmInput = "";
            int searchIndex = 0;
            int searchIndexTransaction = 0;
            int intTest = 0;
            int transactionFileLength = File.ReadAllLines("transactions.txt").Length;
            DateTime currentTime = DateTime.Today;

            Console.WriteLine("Enter email address:");
            Console.WriteLine("'Cancel' to Cancel");
            Console.Write("-- ");
            customerEmailInput = Console.ReadLine();

            if (customerEmailInput.ToUpper() == "CANCEL")
            {
                return myTransactions;
            }

            int testRentalCount = 0;
            int otherRentalCount = 0;
            
            Console.WriteLine();
            Console.WriteLine("Books currently checked out:");
            string[] lines = File.ReadAllLines("transactions.txt");
            if(lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string[] transactionInfo = line.Split('#');
                    if(transactionInfo[3] == customerEmailInput)
                    {
                        if(transactionInfo[5] == "0/0/0000")
                        {
                            searchIndex = booksUtility.SequentialSearch(Convert.ToDouble(transactionInfo[1]));
                            Console.WriteLine(myBooks[searchIndex]);
                        }
                        else if(transactionInfo[5] != "0/0/0000")
                        {
                            otherRentalCount++;
                        }
                    }
                }
                foreach (string line in lines)
                {
                    string[] transactionInfo = line.Split('#');
                    if(transactionInfo[5] != "0/0/0000")
                    {
                        testRentalCount++;
                    }
                }
                if (testRentalCount == 0)
                {
                    Console.WriteLine("Error: There are no books that need to be returned");
                    return myTransactions;
                }
                if (otherRentalCount != 0)
                {
                    Console.WriteLine("Error: There are no books checked out under this email");
                    return myTransactions;
                }
            }
            else if(lines.Length == 0)
            {
                Console.WriteLine("Error: There are no books currently checked out");
            }

            Console.WriteLine();
            Console.WriteLine("Enter the ISBN");
            Console.WriteLine("'Cancel' to Cancel");
            do {
                Console.Write("-- ");
                isbnInput = Console.ReadLine();

                if (isbnInput.ToUpper() == "CANCEL")
                {
                    return myTransactions;
                }

                if (!int.TryParse(isbnInput, out intTest))
                {
                    Console.WriteLine($"Error: {isbnInput} is not a number, Try Again:");
                }
            } while (!int.TryParse(isbnInput, out intTest));  

            searchIndex = booksUtility.SequentialSearch(Convert.ToDouble(isbnInput));
            myBooks = booksFile.GetAllBooks();

            // int searchIndexTransaction = transactionsUtility.SequentialSearch(Convert.ToDouble(isbnInput));
            myTransactions = transactionsFile.GetAllTransactions();

            Console.WriteLine();
            Console.WriteLine($"Customer Email: {customerEmailInput}");
            Console.WriteLine($"Book Return: {myBooks[searchIndex].GetTitle()}");

            Console.WriteLine();
            Console.WriteLine("Would you like to return this book?");
            Console.WriteLine("'Return' to Confirm Return");
            Console.WriteLine("'Cancel' to Cancel Return");
            do 
            {
                Console.Write("-- ");
                confirmInput = Console.ReadLine();
                switch(confirmInput.ToUpper())
                {
                    case "RETURN":
                        // int transactionFileLength2 = File.ReadAllLines("transactions.txt").Length;
                        // string[] tempTransactionsFile = File.ReadAllLines("transactions.txt");
                        // myTransactions[searchIndexTransaction].SetReturnDate(currentTime.ToString());
                        // tempTransactionsFile[searchIndexTransaction]=($"{myTransactions[searchIndexTransaction].GetRentalId()}#{myTransactions[searchIndexTransaction].GetIsbn()}#{myTransactions[searchIndexTransaction].GetCustomerName()}#{myTransactions[searchIndexTransaction].GetCustomerEmail()}#{myTransactions[searchIndexTransaction].GetRentalDate()}#{myTransactions[searchIndexTransaction].GetReturnDate()}");
                        // File.Create("transactions.txdt").Close();
                        // using (StreamWriter sw = File.AppendText("transactions.txt"))
                        // {
                        //     for (int i = 0; i!= transactionFileLength2; i++)
                        //     {
                        //         sw.WriteLine(tempTransactionsFile[i]);
                        //     }
                        // }
                        
                        string[] newLines = File.ReadAllLines("transactions.txt");
                        if(newLines.Length != 0)
                        {
                            foreach (string line in newLines)
                            {
                                string[] transactionInfo = line.Split('#');
                                if(transactionInfo[1] == isbnInput)
                                {
                                    myTransactions = transactionsFile.GetAllTransactions();
                                    if(transactionInfo[5] == "0/0/0000")
                                    {
                                        searchIndexTransaction = transactionsUtility.SequentialSearch(Convert.ToDouble(isbnInput));
                                        Console.WriteLine(searchIndexTransaction);
                                        transactionInfo[5] = currentTime.ToString();
                                        transactionFileLength = File.ReadAllLines("transactions.txt").Length;
                                        string[] transactionFile = File.ReadAllLines("transactions.txt");
                                        transactionFile[searchIndex] = ($"{myTransactions[searchIndexTransaction].GetRentalId()}#{myTransactions[searchIndexTransaction].GetIsbn()}#{myTransactions[searchIndexTransaction].GetCustomerName()}#{myTransactions[searchIndexTransaction].GetCustomerEmail()}#{myTransactions[searchIndexTransaction].GetRentalDate()}#{myTransactions[searchIndexTransaction].GetReturnDate()}");
                                        File.Create("transactions.txt").Close();

                                        using (StreamWriter sw = File.AppendText("transaction.txt"))
                                        {
                                            for (int i=0; i != transactionFileLength; i++)
                                            {
                                                sw.WriteLine(transactionFile[i]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        myTransactions = transactionsFile.GetAllTransactions();
                        Console.WriteLine();
                        Console.WriteLine("Book has been returned.");
                        Console.WriteLine($"Return Date: {myTransactions[searchIndexTransaction].GetReturnDate()}");
                        return myTransactions;
                    case "CANCEL":
                        return myTransactions;
                    default:
                        Console.WriteLine("Error: Invalid Selection, Try Again");
                        break;
                }


            } while (confirmInput.ToUpper() != "RETURN" && confirmInput.ToUpper() != "CANCEL");

            return myTransactions;
        }
    }

}