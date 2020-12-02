using System;
using System.IO;
using System.Globalization;

namespace pleasework
{
    public class BooksFile
    {
        private string fileName;

        public BooksFile(string fileName)
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

        // this method just initializes the myBooks array, as well 
        // as the starting book count
        public Books[] GetAllBooks()
        {
            Books[] myBooks = new Books[200];
            Books.SetCount(0);
            StreamReader inFile = new StreamReader(fileName);
            string[] input = File.ReadAllLines(fileName);
            foreach (string line in input)
            {
                string[] bookInfo = line.Split('#');
                myBooks[Books.GetCount()] = new Books(double.Parse(bookInfo[0]), bookInfo[1], bookInfo[2], bookInfo[3], double.Parse(bookInfo[4]), double.Parse(bookInfo[5]));
                Books.IncCount();
            }
            inFile.Close();
            return myBooks;
        }
        
        // this is the method that adds a new book, or just increases the copies
        // count if it already exists
        public static Books[] AddBook()
        {
            BooksFile booksFile = new BooksFile("books.txt");
            Books[] myBooks = booksFile.GetAllBooks();

            string isbnInput = "";
            string titleInput = "";
            string authorInput = "";
            string genreInput = "";
            string runTimeInput = "";
            string copiesInput = "";
            string addBookInput = "";
            int intTest = 0;

            Console.WriteLine();

            do {
                Console.WriteLine("Enter the ISBN:");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                isbnInput = Console.ReadLine();

                if (isbnInput.Length == 10)
                {
                    break;
                }
                else if (isbnInput.ToUpper() == "CANCEL")
                {
                    return myBooks;
                }

                if (!int.TryParse(isbnInput, out intTest))
                    {
                        Console.Clear();
                        Console.WriteLine($"Error: '{isbnInput}' is not a number, Try Again.");
                        continue;
                    }

                // ISBN Error Handling messages
                if (isbnInput.Length != 10)
                {
                    if (isbnInput.Length < 10)
                    {
                        Console.Clear();
                        Console.WriteLine($"Error: '{isbnInput}' is less than 10 digits, Try Again.");
                    }
                    else if (isbnInput.Length > 10)
                    {
                        Console.Clear();
                        Console.WriteLine($"Error: '{isbnInput}' is more than 10 digits, Try Again.");
                    }
                }
            } while (isbnInput.Length != 10 || isbnInput.ToUpper() != "CANCEL");

            Console.Clear();
            Console.WriteLine("Book Information:");
            Console.WriteLine($"ISBN: {isbnInput}");
            Console.WriteLine();

            bool titleRun = true;
            
            do {
                Console.WriteLine("Enter the Title:");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                titleInput = Console.ReadLine();

                if (titleInput.ToUpper() == "CANCEL")
                {
                    titleRun = false;
                    return myBooks;
                }
                else
                {
                    titleRun = false;
                }

            } while (titleRun == true);

            Console.Clear();
            Console.WriteLine("Book Information:");
            Console.WriteLine($"ISBN: {isbnInput}");
            Console.WriteLine($"Title: {titleInput}");
            Console.WriteLine();

            bool authorRun = true;

            do {
                Console.WriteLine("Enter the Author's Name:");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                authorInput = Console.ReadLine();

                if (authorInput.ToUpper() == "CANCEL")
                {
                    authorRun = false;
                    return myBooks;
                }
                else
                {
                    authorRun = false;
                }

            } while (authorRun == true);

            Console.Clear();
            Console.WriteLine("Book Information:");
            Console.WriteLine($"ISBN: {isbnInput}");
            Console.WriteLine($"Title: {titleInput}");
            Console.WriteLine($"Author: {authorInput}");
            Console.WriteLine();

            bool genreRun = true;

            do {
                Console.WriteLine("Enter the Genre:");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                genreInput = Console.ReadLine();

                if (genreInput.ToUpper() == "CANCEL")
                {
                    genreRun = false;
                    return myBooks;
                }
                else
                {
                    genreRun = false;
                }

            } while (genreRun == true);

            Console.Clear();
            Console.WriteLine("Book Information:");
            Console.WriteLine($"ISBN: {isbnInput}");
            Console.WriteLine($"Title: {titleInput}");
            Console.WriteLine($"Author: {authorInput}");
            Console.WriteLine($"Genre: {genreInput}");
            Console.WriteLine();

            bool runTimeTest = true;

            do {
                Console.WriteLine("Enter the Total Runtime (in minutes):");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                runTimeInput = Console.ReadLine();

                    // Runtime error handling message / cancel input
                if (runTimeInput.ToUpper() == "CANCEL")
                {
                    return myBooks;
                }
                
                if (!int.TryParse(runTimeInput, out intTest))
                {
                    Console.WriteLine($"Error: '{runTimeInput}' is not a number, Try Again.");
                    runTimeTest = true;
                }
                else
                {
                    runTimeTest = false;
                }
            } while (runTimeTest == true);

            Console.Clear();
            Console.WriteLine("Book Information:");
            Console.WriteLine($"ISBN: {isbnInput}");
            Console.WriteLine($"Title: {titleInput}");
            Console.WriteLine($"Author: {authorInput}");
            Console.WriteLine($"Genre: {genreInput}");
            Console.WriteLine($"Runtime: {runTimeInput} minutes");
            Console.WriteLine();

            bool copiesRun = true;

            do {
                Console.WriteLine("How many copies of this book are you adding?");
                Console.WriteLine("'Cancel' to Cancel");
                Console.Write("-- ");
                copiesInput = Console.ReadLine();

                    // Runtime error handling message / cancel input
                if (copiesInput.ToUpper() == "CANCEL")
                {
                     return myBooks;
                }
                else if (!int.TryParse(copiesInput, out intTest))
                {
                    Console.WriteLine($"Error: '{copiesInput}' is not a number, Try Again.");
                    copiesRun = true;
                }
                else
                {
                    copiesRun = false;
                }
            } while (copiesRun == true);

            Console.Clear();
            Console.WriteLine("Book Information:");
            Console.WriteLine($"ISBN: {isbnInput}");
            Console.WriteLine($"Title: {titleInput}");
            Console.WriteLine($"Author: {authorInput}");
            Console.WriteLine($"Genre: {genreInput}");
            Console.WriteLine($"Runtime: {runTimeInput} minutes");
            Console.WriteLine($"Copies: {copiesInput}");
            Console.WriteLine();

            Console.WriteLine("Would you like to add this book?:");
            Console.WriteLine("'Yes' to Confirm");
            Console.WriteLine("'No' to Cancel");
            do {
                Console.Write("-- ");
                addBookInput = Console.ReadLine();
                switch(addBookInput.ToUpper())
                {
                    case "YES":
                        Console.WriteLine();
                        Console.WriteLine("Book Added:");
                        using (StreamWriter sw = File.AppendText("books.txt"))
                        {
                            sw.WriteLine($"{isbnInput}#{titleInput}#{authorInput}#{genreInput}#{runTimeInput}#{copiesInput}");
                        }
                        int lines = File.ReadAllLines("books.txt").Length;
                        myBooks = booksFile.GetAllBooks();
                        Console.WriteLine($"{myBooks[lines-1].ToString()}");
                        return myBooks;
                    case "NO":
                        Console.WriteLine("Thank you, Goodbye!");
                        return myBooks;
                    default:
                        Console.WriteLine($"Error {addBookInput} is not a valid selection, Try Again");
                        break;
                }

            } while(addBookInput.ToUpper() != "NO" && addBookInput.ToUpper() != "YES");
            return myBooks;
        }
  
        // the method that edits a book, this method prompts
        // the user for a specific value to edit, and it allows
        // them to enter in a new value. this includes error handling 
        // as well as updating and re-reading the same original Books.TXT file. 
        public static Books[] EditBook()
        {
            BooksFile booksFile = new BooksFile("books.txt");
            Books[] myBooks = booksFile.GetAllBooks();
            BooksReports booksReports = new BooksReports(myBooks);
            BooksUtility booksUtility = new BooksUtility(myBooks);

            string isbnInput = "";
            string titleInput = "";
            string authorInput = "";
            string genreInput = "";
            string runTimeInput = "";
            string continueInput = "";
            string editInput = "";
            string copiesInput = "";
            double isbnDoubleInput = 0;
            int searchIndex = 0;
            int intTest = 0;

            Console.WriteLine();
            booksReports.PrintAllBooks();
            Console.WriteLine();

            Console.WriteLine("Select an ISBN to Edit:");
            Console.WriteLine("'Cancel' to Cancel");
            do {
                Console.Write("-- ");
                isbnInput = Console.ReadLine();
                if (isbnInput.ToUpper() == "CANCEL")
                {
                    return myBooks;
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

            string isbnValue = myBooks[searchIndex].GetIsbn().ToString();
            string titleValue = myBooks[searchIndex].GetTitle().ToString();
            string authorValue = myBooks[searchIndex].GetAuthor().ToString();
            string genreValue = myBooks[searchIndex].GetGenre().ToString();
            string runTimeValue = myBooks[searchIndex].GetRuntime().ToString();
            string copiesValue = myBooks[searchIndex].GetCopies().ToString();

            Console.WriteLine("Which value would you like to edit?");
            Console.WriteLine();
            Console.WriteLine($"ISBN : {isbnValue}");
            Console.WriteLine($"Title : {titleValue}");
            Console.WriteLine($"Author : {authorValue}");
            Console.WriteLine($"Genre : {genreValue}");
            Console.WriteLine($"Runtime : {runTimeValue}");
            Console.WriteLine($"Copies : {copiesValue}");
            Console.WriteLine();

            Console.WriteLine("'Cancel' to Cancel");
            Console.WriteLine("Enter either the category or specific value:");

            do {

                Console.Write("-- ");
                editInput = Console.ReadLine();

                if (editInput.ToUpper() == "CANCEL")
                {
                    // return myBooks;
                }
                else if (editInput.ToUpper() != "CANCEL" && editInput.ToUpper() != "ISBN" && editInput.ToUpper() != isbnValue.ToUpper() && editInput.ToUpper() != "TITLE" && editInput.ToUpper() != titleValue.ToUpper() && editInput.ToUpper() != "AUTHOR" && editInput.ToUpper() != authorValue.ToUpper() && editInput.ToUpper() != "GENRE" && editInput.ToUpper() != genreValue.ToUpper() && editInput.ToUpper() != "RUNTIME" && editInput.ToUpper() != runTimeValue.ToUpper() && editInput.ToUpper() != "COPIES" && editInput.ToUpper() != copiesValue.ToUpper())
                {
                    Console.WriteLine($"Error: {editInput} is not a valid selection, Try Again");
                }

            } while (editInput.ToUpper() != "CANCEL" && editInput.ToUpper() != "ISBN" && editInput.ToUpper() != isbnValue.ToUpper() && editInput.ToUpper() != "TITLE" && editInput.ToUpper() != titleValue.ToUpper() && editInput.ToUpper() != "AUTHOR" && editInput.ToUpper() != authorValue.ToUpper() && editInput.ToUpper() != "GENRE" && editInput.ToUpper() != genreValue.ToUpper() && editInput.ToUpper() != "RUNTIME" && editInput.ToUpper() != runTimeValue.ToUpper() && editInput.ToUpper() != "COPIES" && editInput.ToUpper() != copiesValue.ToUpper());

                if (editInput.ToUpper() == "ISBN" || editInput.ToUpper() == isbnValue.ToUpper())
                {
                    Console.WriteLine($"Current ISBN: {isbnValue}");
                    do {
                        Console.WriteLine("Enter the new ISBN:");
                        Console.WriteLine("'Cancel' to Cancel");
                        Console.Write("-- ");
                        isbnInput = Console.ReadLine();

                        if (isbnInput.Length == 10)
                        {
                            break;
                        }
                        else if (isbnInput.ToUpper() == "CANCEL")
                        {
                            return myBooks;
                        }

                            // ISBN Error Handling messages
                        if (isbnInput.Length != 10)
                        {
                            if (!int.TryParse(isbnInput, out intTest))
                            {
                                Console.Clear();
                                Console.WriteLine($"Error: '{isbnInput}' is not a number, Try Again.");
                            }
                            else if (isbnInput.Length < 10)
                            {
                                Console.Clear();
                                Console.WriteLine($"Error: '{isbnInput}' is less than 10 digits, Try Again.");
                            }
                            else if (isbnInput.Length > 10)
                            {
                                Console.Clear();
                                Console.WriteLine($"Error: '{isbnInput}' is more than 10 digits, Try Again.");
                            }
                        }
                    } while (isbnInput.Length != 10 || isbnInput.ToUpper() != "CANCEL");
                    Console.Clear();
                    Console.WriteLine($"Old ISBN: {isbnValue}");
                    Console.WriteLine($"New ISBN: {isbnInput}");
                    Console.WriteLine();
                    Console.WriteLine("'Confirm' to Change");
                    Console.WriteLine("'Cancel' to Cancel Change");
                    Console.WriteLine("Enter your selection:");

                    do {

                        Console.Write("-- ");
                        continueInput = Console.ReadLine();

                        switch(continueInput.ToUpper())
                        {
                            case "CONFIRM":
                                int bookFileLength = File.ReadAllLines("books.txt").Length;
                                string[] tempBooks = File.ReadAllLines("books.txt");
                                myBooks[searchIndex].SetIsbn(Convert.ToDouble(isbnInput));
                                tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                                File.Create("books.txt").Close();
                                using (StreamWriter sw = File.AppendText("books.txt"))
                                {
                                    for (int i = 0; i!= bookFileLength; i++)
                                    {
                                        sw.WriteLine(tempBooks[i]);
                                    }
                                }
                                myBooks = booksFile.GetAllBooks();
                                Console.WriteLine();
                                Console.WriteLine("Book has been edited, new value is:");
                                Console.WriteLine(myBooks[searchIndex].ToString());
                                return myBooks;
                            case "CANCEL":
                                break;
                            default:
                                Console.WriteLine("Error: Invalid Selection, Try Again");
                                break;
                        }

                    } while (continueInput.ToUpper() != "CANCEL" && continueInput.ToUpper() != "CONFIRM");

                }
                else if (editInput.ToUpper() == "TITLE" || editInput.ToUpper() == titleValue.ToUpper())
                {
                    Console.WriteLine($"Current Title: {titleValue}");

                    Console.WriteLine("Enter the new Title:");
                    Console.WriteLine("'Cancel' to Cancel");
                    Console.Write("-- ");
                    titleInput = Console.ReadLine();

                    if (titleInput.ToUpper() == "CANCEL")
                    {
                        return myBooks;
                    }
                    
                    Console.Clear();
                    Console.WriteLine($"Old Title: {titleValue}");
                    Console.WriteLine($"New Title: {titleInput}");
                    Console.WriteLine();
                    Console.WriteLine("'Confirm' to Change");
                    Console.WriteLine("'Cancel' to Cancel Change");
                    Console.WriteLine("Enter your selection:");

                    do {

                        Console.Write("-- ");
                        continueInput = Console.ReadLine();

                        switch(continueInput.ToUpper())
                        {
                            case "CONFIRM":
                                int bookFileLength = File.ReadAllLines("books.txt").Length;
                                string[] tempBooks = File.ReadAllLines("books.txt");
                                myBooks[searchIndex].SetTitle(titleInput);
                                tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                                File.Create("books.txt").Close();
                                using (StreamWriter sw = File.AppendText("books.txt"))
                                {
                                    for (int i = 0; i!= bookFileLength; i++)
                                    {
                                        sw.WriteLine(tempBooks[i]);
                                    }
                                }
                                myBooks = booksFile.GetAllBooks();
                                Console.WriteLine();
                                Console.WriteLine("Book has been edited, new value is:");
                                Console.WriteLine(myBooks[searchIndex].ToString());
                                return myBooks;
                            case "CANCEL":
                                break;
                            default:
                                Console.WriteLine("Error: Invalid Selection, Try Again");
                                break;
                        }

                    } while (continueInput.ToUpper() != "CANCEL" && continueInput.ToUpper() != "CONFIRM");
                }
                else if (editInput.ToUpper() == "AUTHOR" || editInput.ToUpper() == authorValue.ToUpper())
                {
                    Console.WriteLine($"Current Author: {authorValue}");

                    Console.WriteLine("Enter the new Author:");
                    Console.WriteLine("'Cancel' to Cancel");
                    Console.Write("-- ");
                    authorInput = Console.ReadLine();

                    if (authorInput.ToUpper() == "CANCEL")
                    {
                        return myBooks;
                    }
                    
                    Console.Clear();
                    Console.WriteLine($"Old Author: {authorValue}");
                    Console.WriteLine($"New Author: {authorInput}");
                    Console.WriteLine();
                    Console.WriteLine("'Confirm' to Change");
                    Console.WriteLine("'Cancel' to Cancel Change");
                    Console.WriteLine("Enter your selection:");

                    do {

                        Console.Write("-- ");
                        continueInput = Console.ReadLine();

                        switch(continueInput.ToUpper())
                        {
                            case "CONFIRM":
                                int bookFileLength = File.ReadAllLines("books.txt").Length;
                                string[] tempBooks = File.ReadAllLines("books.txt");
                                myBooks[searchIndex].SetAuthor(authorInput);
                                tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                                File.Create("books.txt").Close();
                                using (StreamWriter sw = File.AppendText("books.txt"))
                                {
                                    for (int i = 0; i!= bookFileLength; i++)
                                    {
                                        sw.WriteLine(tempBooks[i]);
                                    }
                                }
                                myBooks = booksFile.GetAllBooks();
                                Console.WriteLine();
                                Console.WriteLine("Book has been edited, new value is:");
                                Console.WriteLine(myBooks[searchIndex].ToString());
                                return myBooks;
                            case "CANCEL":
                                break;
                            default:
                                Console.WriteLine("Error: Invalid Selection, Try Again");
                                break;
                        }

                    } while (continueInput.ToUpper() != "CANCEL" && continueInput.ToUpper() != "CONFIRM");
                }
                else if (editInput.ToUpper() == "GENRE" || editInput.ToUpper() == genreValue.ToUpper())
                {
                    Console.WriteLine($"Current Genre: {genreValue}");

                    Console.WriteLine("Enter the new Genre:");
                    Console.WriteLine("'Cancel' to Cancel");
                    Console.Write("-- ");
                    genreInput = Console.ReadLine();

                    if (genreInput.ToUpper() == "CANCEL")
                    {
                        return myBooks;
                    }
                    
                    Console.Clear();
                    Console.WriteLine($"Old Genre: {genreValue}");
                    Console.WriteLine($"New Genre: {genreInput}");
                    Console.WriteLine();
                    Console.WriteLine("'Confirm' to Change");
                    Console.WriteLine("'Cancel' to Cancel Change");
                    Console.WriteLine("Enter your selection:");

                    do {

                        Console.Write("-- ");
                        continueInput = Console.ReadLine();

                        switch(continueInput.ToUpper())
                        {
                            case "CONFIRM":
                                int bookFileLength = File.ReadAllLines("books.txt").Length;
                                string[] tempBooks = File.ReadAllLines("books.txt");
                                myBooks[searchIndex].SetGenre(genreInput);
                                tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                                File.Create("books.txt").Close();
                                using (StreamWriter sw = File.AppendText("books.txt"))
                                {
                                    for (int i = 0; i!= bookFileLength; i++)
                                    {
                                        sw.WriteLine(tempBooks[i]);
                                    }
                                }
                                myBooks = booksFile.GetAllBooks();
                                Console.WriteLine();
                                Console.WriteLine("Book has been edited, new value is:");
                                Console.WriteLine(myBooks[searchIndex].ToString());
                                return myBooks;
                            case "CANCEL":
                                break;
                            default:
                                Console.WriteLine("Error: Invalid Selection, Try Again");
                                break;
                        }

                    } while (continueInput.ToUpper() != "CANCEL" && continueInput.ToUpper() != "CONFIRM");
                }
                else if (editInput.ToUpper() == "RUNTIME" || editInput.ToUpper() == runTimeValue.ToUpper())
                {
                    Console.WriteLine($"Current Runtime: {runTimeValue}");

                    Console.WriteLine("Enter the new Runtime:");
                    Console.WriteLine("'Cancel' to Cancel");
                    do {
                        Console.Write("-- ");
                        runTimeInput = Console.ReadLine();

                        if (genreInput.ToUpper() == "CANCEL")
                        {
                            return myBooks;
                        }
                        else if (!int.TryParse(runTimeInput, out intTest))
                        {
                            Console.WriteLine($"Error: {runTimeInput} is not a number, Try Again");
                        }
                    } while (runTimeInput != "CANCEL" && (!int.TryParse(runTimeInput, out intTest)));
                    
                    Console.Clear();
                    Console.WriteLine($"Old Runtime: {runTimeValue}");
                    Console.WriteLine($"New Runtime: {runTimeInput}");
                    Console.WriteLine();
                    Console.WriteLine("'Confirm' to Change");
                    Console.WriteLine("'Cancel' to Cancel Change");
                    Console.WriteLine("Enter your selection:");

                    do {

                        Console.Write("-- ");
                        continueInput = Console.ReadLine();

                        switch(continueInput.ToUpper())
                        {
                            case "CONFIRM":
                                int bookFileLength = File.ReadAllLines("books.txt").Length;
                                string[] tempBooks = File.ReadAllLines("books.txt");
                                myBooks[searchIndex].SetRuntime(Convert.ToDouble(runTimeInput));
                                tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                                File.Create("books.txt").Close();
                                using (StreamWriter sw = File.AppendText("books.txt"))
                                {
                                    for (int i = 0; i!= bookFileLength; i++)
                                    {
                                        sw.WriteLine(tempBooks[i]);
                                    }
                                }
                                myBooks = booksFile.GetAllBooks();
                                Console.WriteLine();
                                Console.WriteLine("Book has been edited, new value is:");
                                Console.WriteLine(myBooks[searchIndex].ToString());
                                return myBooks;
                            case "CANCEL":
                                break;
                            default:
                                Console.WriteLine("Error: Invalid Selection, Try Again");
                                break;
                        }

                    } while (continueInput.ToUpper() != "CANCEL" && continueInput.ToUpper() != "CONFIRM");Console.WriteLine($"Current Genre: {genreValue}");
                }
                else if (editInput.ToUpper() == "COPIES" || editInput.ToUpper() == copiesValue.ToUpper())
                {
                    Console.WriteLine($"Current Copies: {copiesValue}");

                    Console.WriteLine("Enter the new amount of Copies:");
                    Console.WriteLine("'Cancel' to Cancel");
                    do {
                        Console.Write("-- ");
                        copiesInput = Console.ReadLine();

                        if (copiesInput.ToUpper() == "CANCEL")
                        {
                            return myBooks;
                        }
                        else if (!int.TryParse(copiesInput, out intTest))
                        {
                            Console.WriteLine($"Error: {copiesInput} is not a number, Try Again");
                        }
                    } while (copiesInput.ToUpper() != "CANCEL" && (!int.TryParse(copiesInput, out intTest)));
                    
                    Console.Clear();
                    Console.WriteLine($"Old Copies: {copiesValue}");
                    Console.WriteLine($"New Copies: {copiesInput}");
                    Console.WriteLine();
                    Console.WriteLine("'Confirm' to Change");
                    Console.WriteLine("'Cancel' to Cancel Change");
                    Console.WriteLine("Enter your selection:");

                    do {

                        Console.Write("-- ");
                        continueInput = Console.ReadLine();

                        switch(continueInput.ToUpper())
                        {
                            case "CONFIRM":
                                int bookFileLength = File.ReadAllLines("books.txt").Length;
                                string[] tempBooks = File.ReadAllLines("books.txt");
                                myBooks[searchIndex].SetCopies(Convert.ToDouble(copiesInput));
                                tempBooks[searchIndex] = ($"{myBooks[searchIndex].GetIsbn()}#{myBooks[searchIndex].GetTitle()}#{myBooks[searchIndex].GetAuthor()}#{myBooks[searchIndex].GetGenre()}#{myBooks[searchIndex].GetRuntime()}#{myBooks[searchIndex].GetCopies()}");
                                File.Create("books.txt").Close();
                                using (StreamWriter sw = File.AppendText("books.txt"))
                                {
                                    for (int i = 0; i!= bookFileLength; i++)
                                    {
                                        sw.WriteLine(tempBooks[i]);
                                    }
                                }
                                myBooks = booksFile.GetAllBooks();
                                Console.WriteLine();
                                Console.WriteLine("Book has been edited, new value is:");
                                Console.WriteLine(myBooks[searchIndex].ToString());
                                return myBooks;
                            case "CANCEL":
                                break;
                            default:
                                Console.WriteLine("Error: Invalid Selection, Try Again");
                                break;
                        }

                    } while (continueInput.ToUpper() != "CANCEL" && continueInput.ToUpper() != "CONFIRM");
                }

            return myBooks;
        }
    }
}