using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace pleasework
{
    class Program
    {
            // call in all other method files as objects (i believe this is the way its phrased)
        public static BooksFile booksFile = new BooksFile("books.txt");
        public static Books[] myBooks = booksFile.GetAllBooks();
        public static BooksUtility booksUtility = new BooksUtility(myBooks);
        public static BooksReports booksReports = new BooksReports(myBooks);

            // same as above, just to call in transaction files/methods/objects stuff
        public static TransactionsFile transactionsFile = new TransactionsFile("transactions.txt");
        public static Transactions[] myTransactions = transactionsFile.GetAllTransactions();
        public static TransactionsUtility transactionsUtility = new TransactionsUtility(myTransactions);
        public static TransactionsReports transactionsReports = new TransactionsReports(myTransactions);

        static void Main(string[] args)
        {
            // Sample methods I made to aid in my testing
            // I included these as an extra
                // PopulateTestBooks();
                // ResetBooks();
                // ResetTransactions();
                // PopulateTestTransactions();

            // calls the menu method, which leads to everything else
            MenuDisplay();

        }

            // method to generate any particular amount of random books for testing
        static void PopulateTestBooks()
        {
            Console.WriteLine();

            int length = File.ReadAllLines("books.txt").Length;
            int count = File.ReadAllLines("books.txt").Length;
            string userInput;
            int userIntInput = 0;
            bool runProgram = true;
            bool intTest = true;
            int num = -1;

            do
            {
                Console.WriteLine("[Populate Test Cases - Books.TXT]");
                do
                {
                    Console.WriteLine("How many sample books would you like to add?");
                    Console.WriteLine("Enter 'Cancel' to cancel");
                    Console.Write("-- "); 
                    userInput = Console.ReadLine();
                    if (userInput.ToUpper() == "CANCEL")
                    {
                        runProgram = false;
                        intTest = false;
                        Console.WriteLine("Thank you, Goodbye!");
                        return;
                    }
                    if (!int.TryParse(userInput, out num))
                    {
                        Console.WriteLine("Error: Not a number, Try Again");
                    }
                    else
                    {
                        userIntInput = int.Parse(userInput);
                        intTest = false;
                    }
                } while (intTest == true);

                for(int i=0;i<userIntInput;i++)
                {
                        // specifically defined random variables to meet 10-digit isbn, 2-digit runtime, and 1-digit copies
                    Random random = new Random();
                    string randomIsbn = "";
                    for(int j=0; j<10; j++)
                    {
                        randomIsbn += random.Next(0,9).ToString();
                    }
                    string randomRunTime = "";
                    for(int k=0; k<2; k++)
                    {
                        randomRunTime += random.Next(0,9).ToString();
                    }
                    int randomCopies = random.Next(1,3);

                    using (StreamWriter writer = new StreamWriter("books.txt", true))
                    {
                        writer.WriteLine($"{randomIsbn}#BookTitle{count+1}#BookAuthor{count+1}#BookGenre{count+1}#{randomRunTime}#{randomCopies}");
                    }
                    count++;
                }
                Console.WriteLine("Books Added:");
                Console.WriteLine($"{length} length");
                for(int l=-1; l<userIntInput-1; l++)
                {
                    Console.WriteLine(userIntInput);
                    Console.WriteLine(myBooks[length].ToString());
                    length++;
                }
                runProgram = false;
            }
            while (runProgram != false);
            // Console.WriteLine("Thank you, Goodbye!");
        }

            // method to generate any particular amount of random transactions for testing
        static void PopulateTestTransactions()
        {
            // method to generate any particular amount of random transactions
            Console.WriteLine();

            int length = File.ReadAllLines("transactions.txt").Length;
            int booksLength = File.ReadAllLines("books.txt").Length;
            int count = File.ReadAllLines("transactions.txt").Length;
            string userInput = "";
            int userIntInput = 0;
            bool runProgram = true;
            bool intTest = true;
            int num = -1;

            do
            {
                Console.WriteLine("[Populate Test Cases - Transactions.TXT]");
                do
                {
                    Console.WriteLine("How many sample transactions would you like to add?");
                    Console.WriteLine("Enter 'Cancel' to cancel");
                    Console.Write("-- "); 
                    userInput = Console.ReadLine();
                    if (userInput.ToUpper() == "CANCEL")
                    {
                        runProgram = false;
                        intTest = false;
                        Console.WriteLine("Thank you, Goodbye!");
                        return;
                    }
                    if (!int.TryParse(userInput, out num))
                    {
                        Console.WriteLine("Error: Not a number, Try Again");
                    }
                    else
                    {
                        userIntInput = int.Parse(userInput);
                        intTest = false;
                    }
                } while (intTest == true);

                for(int i=0;i<userIntInput;i++)
                {
                        // specifically defined random variables to meet 10-digit isbn, 2-digit runtime, and 1-digit copies
                    Random random = new Random();
                    double randomIsbn = 0;
                    double[] randomIsbnArray = new double[booksLength];
                    for(int j=0; j<booksLength; j++)
                    {
                        randomIsbnArray[j] = myBooks[j].GetIsbn();
                        Console.WriteLine(myBooks[j].GetIsbn());
                    }
                    int randomIsbnIndex = random.Next(0, booksLength);
                    randomIsbn = myBooks[randomIsbnIndex].GetIsbn();
                    string randomRentalId = "";
                    for(int m=0; m<4; m++)
                    {
                        randomRentalId += random.Next(0,9).ToString();
                    }
                    DateTime today = DateTime.Today;
                    var tomorrow = today.AddDays(1);

                    using (StreamWriter writer = new StreamWriter("transactions.txt", true))
                    {
                        writer.WriteLine($"{randomRentalId}#{randomIsbn}#CustomerName{count+1}#customer{count+1}@gmail.com#{today:d}#{tomorrow:d}");
                    }
                    count++;
                    randomIsbn = 0;
                }
                Console.WriteLine("Transactions Added:");
                Console.WriteLine($"{length} length");
                runProgram = false;
            }
            while (runProgram != false); 
            myTransactions = transactionsFile.GetAllTransactions();
            for(int l=-1; l<userIntInput-1; l++)
                {
                    Console.WriteLine(myTransactions[length].ToString());
                    length++;
                }
           Console.WriteLine("Thank you, Goodbye!");
        }

            // method to reset books.txt for testing purposes
        static void ResetBooks()
        {
            Console.Clear();

            string userInput = "";

            Console.WriteLine("[Reset Confirmation - Books.TXT]");
            do {
                Console.WriteLine("Enter 'Yes' to Continue");
                Console.WriteLine("Enter 'Cancel' to Exit");
                Console.Write("-- ");
                userInput = Console.ReadLine();
                switch(userInput.ToUpper())
                {
                    case "CANCEL":
                        Console.WriteLine("Thank you, Goodbye!");
                        return;
                    case "YES":
                        File.Create("books.txt").Close();
                        Console.WriteLine("Books.TXT has been reset");
                        return;
                    default:
                        Console.WriteLine($"Error: {userInput} is not a valid selection, Try Again");
                        break;
                }
            } while (userInput.ToUpper() != "CANCEL" && userInput.ToUpper() != "YES");
        }

            // method to reset transactions.txt for testing purposes
        static void ResetTransactions()
        {
            Console.Clear();

            string userInput = "";

            Console.WriteLine("[Reset Confirmation - Transactions.TXT]");
            do {
                Console.WriteLine("Enter 'Yes' to Continue");
                Console.WriteLine("Enter 'Cancel' to Exit");
                Console.Write("-- ");
                userInput = Console.ReadLine();
                switch(userInput.ToUpper())
                {
                    case "CANCEL":
                        Console.WriteLine("Thank you, Goodbye!");
                        return;
                    case "YES":
                        File.Create("transactions.txt").Close();
                        Console.WriteLine("Transactions.TXT has been reset");
                        return;
                    default:
                        Console.WriteLine($"Error: {userInput} is not a valid selection, Try Again");
                        break;
                }
            } while (userInput.ToUpper() != "CANCEL" && userInput.ToUpper() != "YES");
        }

            // method to manage menu and functions
        static void MenuDisplay()
        {
            Console.Clear();

            bool runProgram = true;
            string userInput = "";

            Console.WriteLine("[Audiobook Rental Menu]");
            Console.WriteLine("(A) -- Add New Book");
            Console.WriteLine("(B) -- Book Editor");
            Console.WriteLine("(C) -- Book Rentals");
            Console.WriteLine("(D) -- Book Returns");
            Console.WriteLine("(E) -- Reports");
            Console.WriteLine("(F) -- Exit");
            Console.WriteLine("Select an option to continue:");

            // while loop to run through until valid selection is chosen, or program is exited
            do 
            {
                Console.Write("-- ");
                userInput = Console.ReadLine();
                switch(userInput.ToUpper())
                {
                    case "A":
                        Console.Clear();
                        Console.WriteLine("[Audiobook Rental Menu - Add A Book]");
                        // calls myBooks object from BooksFile to populate / update array
                        BooksFile.AddBook();
                        myBooks = booksFile.GetAllBooks();
                        ContinueProgram();
                        return;
                    case "B":
                        Console.Clear();
                        Console.WriteLine("[Audiobook Rental Menu - Book Editor]");
                        myBooks = booksFile.GetAllBooks();
                        // calls myBooks object and edit book method within object, as well as a way to update the arrays
                        BooksFile.EditBook();
                        myBooks = booksFile.GetAllBooks();
                        ContinueProgram();
                        return;
                    case "C":
                        Console.Clear();
                        Console.WriteLine("[Audiobook Rental Menu - Book Rentals]");
                        myBooks = booksFile.GetAllBooks();
                        // transactionFile object to help populate the transactions file with rental's
                        myTransactions = transactionsFile.GetAllTransactions();
                        transactionsFile.RentBook();
                        ContinueProgram();
                        return;
                    case "D":
                        Console.Clear();
                        Console.WriteLine("[Audiobook Rental Menu - Book Returns]");
                        // the correct and proper way to do all of this using object references and defined methods within other object files
                        // this method re-defines myTransactions via the transactionsFile object, and the return book method
                        myTransactions = transactionsFile.ReturnBook(myTransactions, transactionsFile, transactionsReports, transactionsUtility, myBooks, booksUtility, booksReports);
                        transactionsUtility.SetMyTransactions(myTransactions);
                        transactionsReports.SetMyTransactions(myTransactions);
                        ContinueProgram();
                        return;
                    case "E":
                        Console.Clear();
                        Console.WriteLine("[Audiobook Rental Menu - Book Reports]");
                        // this calls the reportbook method from the transactions file, and references all the relevant object instances
                        transactionsFile.ReportBook(myTransactions, transactionsFile, transactionsReports, transactionsUtility, myBooks, booksUtility, booksReports);
                        ContinueProgram();
                        return;
                    case "F":   
                        Console.Clear();
                        // this exits the program and gives a goodbye
                        Console.WriteLine("Thank you, Goodbye!");
                        runProgram = false;
                        return;
                    default:
                        // test case to ensure that the user input's a valid selection, and to let them know if they do not
                        Console.WriteLine($"Error: {userInput} is not a valid selection, Try Again");
                        runProgram = true;
                        break;
                }
            } while (runProgram == true);
        }

            // method to either exit or return to menu
        static void ContinueProgram()
        {
            string userInput = "";

            Console.WriteLine();
            Console.WriteLine("Would you like to exit or return to menu?");
            Console.WriteLine("(A) -- Return to Menu");
            Console.WriteLine("(B) -- Exit");
            Console.WriteLine("Select an option:");

            do {
                
                Console.Write("-- ");
                userInput = Console.ReadLine();

                // switch case to ensure a valid option is selected, runs through until this happens
                switch(userInput.ToUpper())
                {
                    case "A":
                        // displays the menu
                        MenuDisplay();
                        return;
                    case "B":
                        // exits the program
                        Console.WriteLine("Thank you, Goodbye!");
                        return;
                    default:
                        Console.WriteLine($"Error: {userInput} is not an option, Try Again");
                        break;
                }

            } while (userInput != "A" && userInput != "B");
        }
    }
}
