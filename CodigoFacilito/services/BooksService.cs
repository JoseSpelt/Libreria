using CodigoFacilito.entities;
using System.Globalization;

namespace CodigoFacilito.services
{
    public static class BooksService
    {
        public static List<Book> books = new List<Book>();

        public static string AddBook()
        {
            Console.WriteLine("Agregar Libro");
            Console.WriteLine("Ingrese el titulo del libro");
            string title = Console.ReadLine();
            Console.WriteLine("Ingrese el autor del libro");
            string author = Console.ReadLine();
            Console.WriteLine("Ingrese la categoria del libro");
            string category = Console.ReadLine();

            var book = new Book
            {
                Id = books.Count + 1,
                Title = title,
                Author = author,
                Category = category,
                IsAvaliable = true
            };

            books.Add(book);

            return $"El libro {book.Title} ha sido agregado correctamente";
        }

        public static string UpdateBook()
        {
            Console.WriteLine("Actualizar Libro");
            Console.WriteLine("Ingrese el ID  del libro");
            int id = Convert.ToInt16(Console.ReadLine());
            var book = books.FirstOrDefault(x => x.Id == id);

            if(book == null)
            {
                return $"El libro con ID {id} no existe";
            }
            else
            {
                Console.WriteLine("Ingrese el nuevo titulo del libro");
                string title = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo autor del libro");
                string author = Console.ReadLine();
                Console.WriteLine("Ingrese la nueva categoria del libro");
                string category = Console.ReadLine();

                book.Title = title;
                book.Author = author;
                book.Category = category;

                books.Add(book);
            }

            return $"El libro con el ID {book.Id} ha sido actualizado correctamente";
        }

        public static string GetAll()
        {
            string message = string.Empty;
            Console.WriteLine("Listado de Libros");

            if(books == null)
            {
                message = "No hay libros disponibles";
            }
            foreach (var book in books)
            {
                message = $"ID: {book.Id} \nTitulo: {book.Title} \nAutor: {book.Author} \nCategoria: {book.Category} \nDisponible: {book.IsAvaliable}";

            }

            return message;
        }
    }
}
