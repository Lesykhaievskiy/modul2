
//Variant 2

public interface ILibraryItem {
 void issuance();
 void returnBook();
 void CheckState();
}

public delegate void BookActionHandler(string message);
public class LibraryException : Exception {
    public LibraryException(string message) : base(message) { }
}

public class Book : ILibraryItem
{

    private string _name;
    private string _author;
    private int _year;
    public bool _isIssued;
  

   public event BookActionHandler BookActionEvent;
    public Book(string name, string author, int year, bool isIssued) {
        _name = name;
        _author = author;
        _year = year;
        _isIssued = isIssued;
    }


    public void CheckState()
    {
        if (_isIssued) {
            Console.WriteLine("The book is currently out of stock");
        } else {
            BookActionEvent.Invoke("The book is in library");
        }
    
    }

    public void issuance()
    {
       if (_isIssued) {
        throw new LibraryException("Book is already issued");
        }
        _isIssued = true;
        BookActionEvent.Invoke($"{_name} has been issued.");
    }

    public void returnBook()
{
    if (!_isIssued) {
        throw new LibraryException("You cannot return this book");
    }
    else {
        _isIssued = false;
        BookActionEvent?.Invoke($"{_name} has been returned.");
    }
}
        
}



public class LibraryUser {
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public LibraryUser(string name, string surname, int age) {
        Name = name;
        Surname = surname;
        Age = age;
    }
}


public class Student : LibraryUser
{
    public Student(string name, string surname, int age) : base(name, surname, age){}

}

public class Teacher : LibraryUser
{
    public Teacher(string name, string surname, int age) : base(name, surname, age){}
}



class Program {
    static void Main(string[] args) {
    Book book = new Book("Harry Potter", "Haievskyi", 2024, true);

    book.BookActionEvent += HandleBookAction;

    book.CheckState();
    
    try
    {
        book.returnBook();
    }
    catch (LibraryException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
    book.CheckState();

    try
    {
        book.returnBook();
    }
    catch (LibraryException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    book.issuance();
    book.CheckState();
}


    static void HandleBookAction(string message) {
        Console.WriteLine(message);
    }
}




/*
Розробка системи управління книгами у бібліотеці
Ваша програма повинна містити наступні елементи:
Створення інтерфейсу ILibraryItem, який містить методи для видачі книги, повернення книги та перевірки стану книги.
Створення класу Book, який реалізує інтерфейс ILibraryItem та містить інформацію про книгу (наприклад, назва, автор, рік видання тощо).
Побудова ієрархії класів для користувачів бібліотеки: базовий клас LibraryUser, який містить загальні властивості, та похідні класи, наприклад, Student, Teacher тощо.
Використання конструкторів для ініціалізації об'єктів класів та деструкторів для звільнення ресурсів.
Синхронний виклик методів через делегат для видачі та повернення книг.
Визначення події для сповіщення про зміну статусу книги та організація взаємодії об'єктів через цю подію.
Розробка класу винятків для обробки помилок у випадку виникнення проблем під час видачі або повернення книги.
*/