public interface ILibraryItem
{
    void BorrowBook();
    void ReturnBook();
    string StatusBook();
}

public delegate void StatusBook();

public class BookStatusChangedEventArgs : EventArgs
{
    public string Message { get; }
    public BookStatusChangedEventArgs(string message = "")
    {
        Message = message;
    }
}
public class Book : ILibraryItem
{
    private readonly LibraryList<Book> library = new LibraryList<Book>();
    public event EventHandler<BookStatusChangedEventArgs> BookStatusChanged;

    public string Title { get; set; }
    public bool IsBorrowed { get; set; }
    public Book(string title)
    {
        Title = title;
        IsBorrowed = false;
    }

    public void BorrowBook()
    {
        IsBorrowed = true;
        Console.WriteLine($"Book \"{Title}\" borrowed.");
    }

    public void ReturnBook()
    {
        IsBorrowed = false;
        Console.WriteLine($"Book \"{Title}\" returned.");
    }

    public string StatusBook()
    {
        if (IsBorrowed)
        {
            return $"Book \"{Title}\" borrowed.";
        }
        return $"Book \"{Title}\" not borrowed.";
    }

    protected virtual void OnBookStatusChanged(string message)
    {
        BookStatusChanged?.Invoke(this, new BookStatusChangedEventArgs(message));
    }

    public void ClearLibrary()
    {
        library.ClearLibrary();
    }
    ~Book()
    {
        ClearLibrary();
    }
}

public class LibraryUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public LibraryUser(string name, string surmame)
    {
        Name = name;
        Surname = surmame;
    }
}

public class Student : LibraryUser
{
    public string Group { get; set; }
    public Student(string name, string surname, string group) : base(name, surname)
    {
        Group = group;
    }
}

public class Teacher : LibraryUser
{
    public string Subject { get; set; }
    public Teacher(string name, string surname, string subject) : base(name, surname)
    {
        Subject = subject;
    }
}

public class BookException : Exception
{
    public BookException(string message) : base(message) { }
}

public class LibraryList<T> where T : Book
{
    private readonly List<T> library = new List<T>();

    public void BorrowBook(string title)
    {
        T book = library.Find(b => b.Title == title);
        if (book.IsBorrowed)
        {
            throw new BookException("Book is already borrowed");
        }
        book.IsBorrowed = true;
    }

    public void ReturnBook(string title)
    {
        T book = library.Find(b => b.Title == title);
        if (!book.IsBorrowed)
        {
            throw new BookException("Book is not borrowed");
        }
        book.IsBorrowed = false;
    }
   
    public void PrintLibrary()
    {
        foreach (T book in library)
        {
            Console.WriteLine($"{book.Title}");
        }
    }
    public void ClearLibrary()
    {
        library.Clear();
    }
    public bool ContainsBook(string title)
    {
        if (library.Find(b => b.Title == title) != null)
        {
            return true;
        }
        return false;
    }
    public IEnumerator<T> GetEnumerator()
    {
        return library.GetEnumerator();
    }

    ~LibraryList()
    {
        ClearLibrary();
    }
}
class Program
{
    static void Main(string[] args)
    {
        static int variant(int student) => (student - 1) % 3 + 1;
        Console.WriteLine($"Variant: {variant(14)}");

        Book book = new Book("Title of book");

        StatusBook borrowBook = book.BorrowBook;

        StatusBook returnBook = book.ReturnBook;

        Console.WriteLine("Book is borrowing:");
        borrowBook.Invoke();

        Console.WriteLine("\nBook is returning:");
        returnBook.Invoke();

        book.BookStatusChanged += BookStatusChangedHandler;

        static void BookStatusChangedHandler(object sender, BookStatusChangedEventArgs e)
        {
            Console.WriteLine($"Status was changed: {e.Message}");
        }
    }
}