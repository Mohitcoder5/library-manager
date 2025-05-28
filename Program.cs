using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    class Program
    {
        static List<Book> library = new List<Book>();
        static int bookCounter = 1;

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Library Menu ---");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Display all books");
                Console.WriteLine("3. Search books by title");
                Console.WriteLine("4. List available books");
                Console.WriteLine("5. Mark a book as borrowed");
                Console.WriteLine("6. Mark a book as returned");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");
                
                switch (Console.ReadLine())
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DisplayAllBooks();
                        break;
                    case "3":
                        SearchBooks();
                        break;
                    case "4":
                        ListAvailableBooks();
                        break;
                    case "5":
                        BorrowBook();
                        break;
                    case "6":
                        ReturnBook();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.WriteLine("\n--- Add a New Book ---");
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Author: ");
            string author = Console.ReadLine();

            Console.Write("Publication Year: ");
            if (int.TryParse(Console.ReadLine(), out int year) && year > 0)
            {
                Book book = new Book
                {
                    BookId = bookCounter++,
                    Title = title,
                    Author = author,
                    PublicationYear = year,
                    IsAvailable = true
                };
                library.Add(book);
                Console.WriteLine("Book added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid year. Book not added.");
            }
        }

        static void DisplayAllBooks()
        {
            Console.WriteLine("\n--- All Books ---");
            if (library.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (var book in library)
            {
                DisplayBook(book);
            }
        }

        static void SearchBooks()
        {
            Console.Write("\nEnter title to search: ");
            string keyword = Console.ReadLine().ToLower();
            var results = library.Where(b => b.Title.ToLower().Contains(keyword)).ToList();

            if (results.Any())
            {
                Console.WriteLine("\nSearch Results:");
                foreach (var book in results)
                {
                    DisplayBook(book);
                }
            }
            else
            {
                Console.WriteLine("No books found with that title.");
            }
        }

        static void ListAvailableBooks()
        {
            Console.WriteLine("\n--- Available Books ---");
            var available = library.Where(b => b.IsAvailable).ToList();

            if (available.Any())
            {
                foreach (var book in available)
                {
                    DisplayBook(book);
                }
            }
            else
            {
                Console.WriteLine("No available books.");
            }
        }

        static void BorrowBook()
        {
            Console.Write("\nEnter Book ID to borrow: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = library.FirstOrDefault(b => b.BookId == id);
                if (book != null && book.IsAvailable)
                {
                    book.IsAvailable = false;
                    Console.WriteLine("Book marked as borrowed.");
                }
                else
                {
                    Console.WriteLine("Book not found or already borrowed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void ReturnBook()
        {
            Console.Write("\nEnter Book ID to return: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = library.FirstOrDefault(b => b.BookId == id);
                if (book != null && !book.IsAvailable)
                {
                    book.IsAvailable = true;
                    Console.WriteLine("Book marked as returned.");
                }
                else
                {
                    Console.WriteLine("Book not found or already returned.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void DisplayBook(Book book)
        {
            Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}, Available: {(book.IsAvailable ? "Yes" : "No")}");
        }
    }
}
