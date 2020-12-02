using System;
using System.IO;
using System.Collections.Generic;
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

        // this method just reads in all the transactions
        // and gets the count
        public Transactions[] GetAllTransactions()
        {
            Transactions[] myTransactions = new Transactions[200];
            Transactions.SetCount(0);
            StreamReader inFile = new StreamReader(fileName);
            string[] input = File.ReadAllLines(fileName);
            foreach (string line in input)
            {
                string[] transactionInfo = line.Split('#');
                myTransactions[Transactions.GetCount()] = new Transactions(double.Parse(transactionInfo[0]), transactionInfo[1], transactionInfo[2], transactionInfo[3], transactionInfo[4], transactionInfo[5]);
                Transactions.IncCount();
            }
            inFile.Close();
            return myTransactions;
        }
        

        // method to rent the book, returns myTransactions value
        // this method recalls every object in itself multiple times
        // which I have actually learned not to necessarily do
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
            double rentalId = transactionLength+1;
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

                if (myBooks[searchIndex].GetCopies() == 0)
                {
                    Console.WriteLine("Error: There are no copies left");
                    return myTransactions;
                }

                Console.WriteLine("Checking out:");
                Console.WriteLine($"Rental ID: {rentalId:0000}");
                Console.WriteLine($"Title: {myBooks[searchIndex].GetTitle()}");
                Console.WriteLine();

                Console.WriteLine("Enter Customer Email:");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                customerEmailInput = Console.ReadLine();
                
                if (customerEmailInput.ToUpper() == "CANCEL")
                {
                    return myTransactions;
                }

                string[] lines = File.ReadAllLines("transactions.txt");
                if(lines.Length != 0)
                {
                    foreach (string line in lines)
                    {
                        string[] transactionInfo = line.Split('#');
                        if(transactionInfo[3] == customerEmailInput)
                        {
                            if(transactionInfo[1] == isbnInput)
                            {   
                                Console.WriteLine();
                                Console.WriteLine("Error: You already have a copy of this book");
                                return myTransactions; 
                            }
                        }
                    }
                }

            } while (isbnInput.ToUpper() != "CANCEL" && isbnInput.Length != 10);

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

        // ReturnBook method which properly references and recalls objects
        // this method also properly (or semi-properly) updates everything
        // as well as returning to the main function, or Programs.cs
        public Transactions[] ReturnBook(Transactions[] myTransactions, TransactionsFile transactionsFile, TransactionsReports transactionsReports, TransactionsUtility transactionsUtility, Books[] myBooks, BooksUtility booksUtility, BooksReports booksReports)
        {
            string customerEmailInput = "";
            string isbnInput = "";
            string confirmInput = "";
            int searchIndex = 0;
            int searchIndexTransaction = 0;
            double doubleTest = 0;
            int transactionFileLength = myTransactions.Length;
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
                            // Console.WriteLine(Convert.ToDouble(transactionInfo[1]));
                            searchIndex = booksUtility.SequentialSearch(Convert.ToDouble(transactionInfo[1]));
                            // Console.WriteLine(searchIndex);
                            Console.WriteLine(myBooks[searchIndex]);
                            otherRentalCount++;
                            testRentalCount++;    
                        }
                    }
                }
                // foreach (string line in lines)
                // {
                //     string[] transactionInfo = line.Split('#');
                //     if(transactionInfo[5] != "0/0/0000")
                //     {
                //     }
                // }
                if (testRentalCount == 0)
                {
                    Console.WriteLine("Error: There are no books that need to be returned");
                    return myTransactions;
                }
                if (otherRentalCount == 0)
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



                if (!double.TryParse(isbnInput, out doubleTest))
                {
                    Console.WriteLine($"Error: {isbnInput} is not a number, Try Again:");
                }
            } while (!double.TryParse(isbnInput, out doubleTest));  

            searchIndex = booksUtility.SequentialSearch(Convert.ToDouble(isbnInput));

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
                        
                        string[] newLines = File.ReadAllLines("transactions.txt");
                        if(newLines.Length != 0)
                        {
                            foreach (string line in newLines)
                            {
                                string[] transactionInfo = line.Split('#');
                                if(transactionInfo[1] == isbnInput)
                                {
                                    if(transactionInfo[5] == "0/0/0000")
                                    {
                                        searchIndexTransaction = transactionsUtility.SequentialSearch(isbnInput);
                                        myTransactions[searchIndexTransaction].SetReturnDate(currentTime.ToString());
                                        transactionFileLength = File.ReadAllLines("transactions.txt").Length;
                                        string[] transactionFile = File.ReadAllLines("transactions.txt");
                                        transactionFile[searchIndexTransaction] = ($"{myTransactions[searchIndexTransaction].GetRentalId():0000}#{myTransactions[searchIndexTransaction].GetIsbn()}#{myTransactions[searchIndexTransaction].GetCustomerName()}#{myTransactions[searchIndexTransaction].GetCustomerEmail()}#{myTransactions[searchIndexTransaction].GetRentalDate()}#{myTransactions[searchIndexTransaction].GetReturnDate()}");
                                        File.Create("transactions.txt").Close();

                                        using (StreamWriter sw = File.AppendText("transactions.txt"))
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
                        Console.WriteLine();
                        Console.WriteLine("Book has been returned.");
                        Console.WriteLine($"Return Date: {myTransactions[searchIndexTransaction].GetReturnDate()}");
                        myTransactions = transactionsFile.GetAllTransactions();
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


        // The function that contains the menu for the Reports function, that is also present in the main method
        // this function was also one of the most successful, and one of my most proudest this project
        // I believe I referenced everything correctly
        public void ReportBook(Transactions[] myTransactions, TransactionsFile transactionsFile, TransactionsReports transactionsReports, TransactionsUtility transactionsUtility, Books[] myBooks, BooksUtility booksUtility, BooksReports booksReports)
        {
            
            string userInput = "";

            Console.WriteLine("(A) -- Total rentals by month and year");
            Console.WriteLine("(B) -- Individual Customer Rentals");
            Console.WriteLine("(C) -- Historical Customer Rentals");
            Console.WriteLine("(D) -- Historical Genre Report");
            Console.WriteLine("'Cancel' to Cancel");
            
            do {
                Console.Write("-- ");
                userInput = Console.ReadLine().ToUpper();

                switch(userInput)
                {
                    case "A":
                        TotalMonthYear(booksUtility, myBooks);
                        break;
                    case "B":
                        IndividualRentals(booksUtility, myBooks);
                        break;
                    case "C":
                        HistoricalRentals(booksUtility, myBooks);
                        break;
                    case "D":
                        HistoricalGenre(booksUtility, myBooks);
                        break;
                    case "CANCEL":
                        return;
                    default:
                        Console.WriteLine("Error: Not an option");
                        break;
                }

            } while(userInput != "A" && userInput != "B" && userInput != "C" && userInput != "D");

        }

       // This is the method that prints out all the individual rentals tied
       // to a particular email 
        private void IndividualRentals(BooksUtility booksUtility, Books[] myBooks)
        {
            string customerEmailInput = "";
            int searchIndex = 0;

            Console.WriteLine("Enter email address:");
            Console.WriteLine("'Cancel' to Cancel");
            Console.Write("-- ");
            customerEmailInput = Console.ReadLine();

            if (customerEmailInput.ToUpper() == "CANCEL")
            {
                return;
            }
            
            Console.WriteLine();
            Console.WriteLine("Individual Customer Rentals:");
            string[] lines = File.ReadAllLines("transactions.txt");
            if(lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string[] transactionInfo = line.Split('#');
                    if(transactionInfo[3] == customerEmailInput)
                    {
                        searchIndex = booksUtility.SequentialSearch(Convert.ToDouble(transactionInfo[1]));
                        Console.WriteLine(myBooks[searchIndex]);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Would you like to print this information to a file?");
            Console.WriteLine("'Yes' to Create New File");
            Console.WriteLine("'No' or 'Cancel' to Cancel");
            Console.WriteLine("Select an option:");
            string printInput;
            do
            {  
                Console.Write("-- ");
                printInput = Console.ReadLine().ToUpper();
                switch(printInput)
                {
                    case "YES":
                        File.Create("IndividualCustomerRentals.txt").Close();
                        using(StreamWriter sw = File.CreateText("IndividualCustomerRentals.txt"))
                        {
                            sw.WriteLine("Individual Customer Rentals:");
                            if(lines.Length != 0)
                            {
                                foreach (string line in lines)
                                {
                                    string[] transactionInfo = line.Split('#');
                                    if(transactionInfo[3] == customerEmailInput)
                                    {
                                        searchIndex = booksUtility.SequentialSearch(Convert.ToDouble(transactionInfo[1]));
                                        sw.WriteLine(myBooks[searchIndex]);
                                    }
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("IndividualCustomerRentals.TXT has been created");
                        break;
                    case "NO":
                        return;
                    case "CANCEL":
                        return;
                    default:
                        Console.WriteLine("Error: Invalid Selection");
                        break;
                }

            } while (printInput != "YES" && printInput != "NO" && printInput != "CANCEL");
        }
        // This is the extra method I did, this one prints out a table relatively
        // similar to the other/individual Historical Rentals. This one instead
        // breaks each section down by the overall genre of the book, and prints
        // the amount of rentals per month by year, as well the total overall. 
        private void HistoricalGenre(BooksUtility booksUtility, Books[] myBooks)
        {
            SortedDictionary<string, SortedDictionary<int, int[]>> dictionary = new SortedDictionary<string, SortedDictionary<int, int[]>>();
            
            string[] lines = File.ReadAllLines("transactions.txt");
            if(lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string[] transactionInfo = line.Split('#');
                    string book = transactionInfo[1];
                    string date = transactionInfo[4];
                    int isbnIndex = booksUtility.SequentialSearch(Convert.ToDouble(book));
                    string genre = myBooks[isbnIndex].GetGenre();
                    string[] dates = date.Split('/');
                    int year = int.Parse(dates[2]);
                    int month = int.Parse(dates[0]);
                    SortedDictionary<int, int[]> genres;
                    if(!dictionary.ContainsKey(genre))
                    {
                        genres = new SortedDictionary<int, int[]>();
                        dictionary[genre] = genres;
                    }
                    else
                    {
                        genres = dictionary[genre];
                    }
                    SortedDictionary<int, int[]> years;
                    if(!genres.ContainsKey(year))
                    {
                        years = new SortedDictionary<int, int[]>();
                        genres[year] = new int[12];
                    }
                    genres[year][month-1] += 1;
                }
            }

            string[] monthNames = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

            Console.Clear();
            Console.WriteLine("Historical Genre Report");
            Console.WriteLine();
            foreach(var genre in dictionary)
            {
                Console.WriteLine(genre.Key);
                Console.WriteLine("    \t\t"+String.Join('\t', monthNames));
                Console.WriteLine(new String('-', 110));
                foreach(var year in genre.Value)
                {
                    Console.Write("    \t" + year.Key + "\t");
                    int total = 0;
                    foreach(var month in year.Value)
                    {
                        Console.Write(String.Format("{0,2}", month) + "\t");
                        total += month;
                    }
                    Console.WriteLine();
                    Console.WriteLine(new String('-', 110));
                    Console.WriteLine("    \tTotal" + String.Format("{0,93}", total));
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Would you like to print this information to a file?");
            Console.WriteLine("'Yes' to Create New File");
            Console.WriteLine("'No' or 'Cancel' to Cancel");
            Console.WriteLine("Select an option:");
            string printInput;
            do
            {  
                Console.Write("-- ");
                printInput = Console.ReadLine().ToUpper();
                switch(printInput)
                {
                    case "YES":
                        File.Create("HistoricalGenre.txt").Close();
                        using(StreamWriter sw = File.CreateText("HistoricalGenre.txt"))
                        {
                            sw.WriteLine("Historical Genre Report");
                            sw.WriteLine();
                            foreach(var genre in dictionary)
                            {
                                sw.WriteLine(genre.Key);
                                sw.WriteLine("    \t\t"+String.Join('\t', monthNames));
                                sw.WriteLine(new String('-', 110));
                                foreach(var year in genre.Value)
                                {
                                    sw.Write("    \t" + year.Key + "\t");
                                    int total = 0;
                                    foreach(var month in year.Value)
                                    {
                                        sw.Write(String.Format("{0,2}", month) + "\t");
                                        total += month;
                                    }
                                    sw.WriteLine();
                                    sw.WriteLine(new String('-', 110));
                                    sw.WriteLine("    \tTotal" + String.Format("{0,93}", total));
                                    sw.WriteLine();
                                }
                                Console.WriteLine();
                            }
                        }
                        Console.WriteLine("HistoricalGenre.TXT has been created");
                        break;
                    case "NO":
                        return;
                    case "CANCEL":
                        return;
                    default:
                        Console.WriteLine("Error: Invalid Selection");
                        break;
                }

            } while (printInput != "YES" && printInput != "NO" && printInput != "CANCEL");
        }

        // This was the most basic of all the functions, this one in particular prints
        // out only the totals by the month and year. 
        private void TotalMonthYear(BooksUtility booksUtility, Books[] myBooks)
        {
            
            SortedDictionary<int, int[]> years = new SortedDictionary<int, int[]>();

            string[] lines = File.ReadAllLines("transactions.txt");
            if(lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string[] transactionInfo = line.Split('#');
                    string date = transactionInfo[5];
                    if (date != "0/0/0000")
                    {
                        int year = int.Parse(date.Split('/')[2]);
                        int[] months;
                        if (!years.ContainsKey(year))
                        {
                            months = new int[12];
                            years[year] = months;
                        }
                        months = years[year];
                        int month = int.Parse(date.Split('/')[0]);
                        years[year][month-1] += 1;
                    }
                }
            }
            
            Console.Clear();
            Console.WriteLine("Total Rentals by Month and Year");
            Console.WriteLine(new String('-', 100));
            string[] monthNames = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
            Console.WriteLine("    \t"+String.Join('\t', monthNames));
            Console.WriteLine(new String('-', 100));

            foreach(var key in years)
            {
                string[] months = new string[12];
                int index = 0;
                foreach(var month in key.Value)
                {
                    months[index] = ($"{Convert.ToString(month):00}");
                    index++;
                }
                Console.Write(key.Key);
                Console.Write("\t");
                Console.WriteLine(String.Join('\t', months));
            }

            Console.WriteLine();
            Console.WriteLine("Would you like to print this information to a file?");
            Console.WriteLine("'Yes' to Create New File");
            Console.WriteLine("'No' or 'Cancel' to Cancel");
            Console.WriteLine("Select an option:");
            string printInput;
            do
            {  
                Console.Write("-- ");
                printInput = Console.ReadLine().ToUpper();
                switch(printInput)
                {
                    case "YES":
                        File.Create("TotalRentalsMonthYear.txt").Close();
                        using(StreamWriter sw = File.CreateText("TotalRentalsMonthYear.txt"))
                        {
                            sw.WriteLine("Total Rentals by Month and Year");
                            sw.WriteLine(new String('-', 100));
                            sw.WriteLine("    \t"+String.Join('\t', monthNames));
                            sw.WriteLine(new String('-', 100));

                            foreach(var key in years)
                            {
                                string[] months = new string[12];
                                int index = 0;
                                foreach(var month in key.Value)
                                {
                                    months[index] = ($"{Convert.ToString(month):00}");
                                    index++;
                                }
                                sw.Write(key.Key);
                                sw.Write("\t");
                                sw.WriteLine(String.Join('\t', months));
                            }
                        }
                        Console.WriteLine("TotalRentalsMonthYear.txt has been created");
                        break;
                    case "NO":
                        return;
                    case "CANCEL":
                        return;
                    default:
                        Console.WriteLine("Error: Invalid Selection");
                        break;
                }

            } while (printInput != "YES" && printInput != "NO" && printInput != "CANCEL");
        }

        // This is the method for Historical Rentals, specifically by every book
        // followed by every person that rented the book, followed with when
        // they rented the book, as well as how many copies of the book they rented
        // and how many times it has been rented in total
        private void HistoricalRentals(BooksUtility booksUtility, Books[] myBooks)
        {
            SortedDictionary<string, SortedDictionary<string, SortedDictionary<string, int>>> dictionary = new SortedDictionary<string, SortedDictionary<string, SortedDictionary<string, int>>>();
            
            string[] lines = File.ReadAllLines("transactions.txt");
            if(lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string[] transactionInfo = line.Split('#');
                    string book = transactionInfo[1];
                    string customer =  transactionInfo[2];
                    string date = transactionInfo[4];
                    SortedDictionary<string, SortedDictionary<string, int>> books;
                    if(!dictionary.ContainsKey(book))
                    {
                        books = new SortedDictionary<string, SortedDictionary<string, int>>();
                        dictionary[book] = books;
                    }
                    else
                    {
                        books = dictionary[book];
                    }
                    SortedDictionary<string, int> customers;
                    if(!books.ContainsKey(customer))
                    {
                        customers = new SortedDictionary<string, int>();
                        books[customer] = customers;
                    }
                    else
                    {
                        customers = books[customer];
                    }
                    if (!customers.ContainsKey(date))
                    {
                        customers[date] = 0;
                    }
                    customers[date] += 1;
                }
            }

            Console.Clear();
            Console.WriteLine("Historical Customer Rentals");
            Console.WriteLine(new String('-', 50));
            foreach(var book in dictionary)
            {
                int isbnIndex = booksUtility.SequentialSearch(Convert.ToDouble(book.Key));
                Console.WriteLine(myBooks[isbnIndex].GetTitle());
                foreach(var customer in book.Value)
                {
                    Console.WriteLine("\t" + customer.Key);
                    int total = 0;
                    foreach(var date in customer.Value)
                    {
                        Console.WriteLine($"\t\t{date.Key}\t\t" + String.Format("{0,2}", date.Value));
                        total++;
                    }
                    Console.WriteLine(new String('-', 50));
                    Console.WriteLine("\t\t          \t\t" + String.Format("{0,2}", total));
                }
            }

            Console.WriteLine();
            Console.WriteLine("Would you like to print this information to a file?");
            Console.WriteLine("'Yes' to Create New File");
            Console.WriteLine("'No' or 'Cancel' to Cancel");
            Console.WriteLine("Select an option:");
            string printInput;
            do
            {  
                Console.Write("-- ");
                printInput = Console.ReadLine().ToUpper();
                switch(printInput)
                {
                    case "YES":
                        File.Create("HistoricalCustomerRentals.txt").Close();
                        using(StreamWriter sw = File.CreateText("HistoricalCustomerRentals.txt"))
                        {
                            sw.WriteLine("Historical Customer Rentals");
                            sw.WriteLine(new String('-', 50));
                            foreach(var book in dictionary)
                            {
                                int isbnIndex = booksUtility.SequentialSearch(Convert.ToDouble(book.Key));
                                sw.WriteLine(myBooks[isbnIndex].GetTitle());
                                foreach(var customer in book.Value)
                                {
                                    sw.WriteLine("\t" + customer.Key);
                                    int total = 0;
                                    foreach(var date in customer.Value)
                                    {
                                        sw.WriteLine($"\t\t{date.Key}\t\t" + String.Format("{0,2}", date.Value));
                                        total++;
                                    }
                                    sw.WriteLine(new String('-', 50));
                                    sw.WriteLine("\t\t          \t\t" + String.Format("{0,2}", total));
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("HistoricalCustomerRentals.txt has been created");
                        break;
                    case "NO":
                        return;
                    case "CANCEL":
                        return;
                    default:
                        Console.WriteLine("Error: Invalid Selection");
                        break;
                }

            } while (printInput != "YES" && printInput != "NO" && printInput != "CANCEL");

            
        }
    }
}