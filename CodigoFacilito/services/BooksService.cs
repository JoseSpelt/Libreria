using CodigoFacilito.entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodigoFacilito.services
{
    public static class BooksService
    {
        private const string JsonFilePath = @"C:\C\BOOKS.JSON";
        private static List<Book> books = new List<Book>();

        public static string AddBook()
        {
            Console.WriteLine("Agregar Libro");
            Console.WriteLine("Ingrese el titulo del libro");
            string? title = Console.ReadLine();
            Console.WriteLine("Ingrese el autor del libro");
            string? author = Console.ReadLine();
            Console.WriteLine("Ingrese la categoria del libro");
            string? category = Console.ReadLine();

            // Cargar libros existentes del archivo
            LoadBooks();

            // Crear el nuevo objeto Book
            var newBook = new Book
            {
                Id = books.Count + 1,
                Title = title,
                Author = author,
                Category = category,
                IsAvaliable = true
            };

            // Agregar el nuevo libro a la lista
            books.Add(newBook);

            // Guardar la lista actualizada de libros en el archivo JSON
            SaveBooks();

            return $"El libro '{newBook.Title}' ha sido agregado correctamente";
        }

        public static string UpdateBook()
        {
            Console.WriteLine("Actualizar Libro");
            Console.WriteLine("Ingrese el ID del libro a actualizar:");
            int id = Convert.ToInt32(Console.ReadLine());

            // Cargar libros existentes del archivo
            LoadBooks();

            // Buscar el libro por ID
            var bookToUpdate = books.FirstOrDefault(b => b.Id == id);

            if (bookToUpdate == null)
            {
                return $"No se encontró ningún libro con el ID {id}";
            }

            Console.WriteLine($"Libro encontrado: {bookToUpdate.Title} por {bookToUpdate.Author}");
            Console.WriteLine("Ingrese el nuevo titulo del libro");
            string title = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo autor del libro");
            string author = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva categoria del libro");
            string category = Console.ReadLine();

            // Actualizar el libro encontrado
            bookToUpdate.Title = title;
            bookToUpdate.Author = author;
            bookToUpdate.Category = category;

            // Guardar la lista actualizada de libros en el archivo JSON
            SaveBooks();

            return $"El libro con ID {id} ha sido actualizado correctamente";
        }

        public static string GetAll()
        {
            // Cargar libros existentes del archivo
            LoadBooks();

            if (books.Any())
            {
                Console.WriteLine("Listado de libros:");
                foreach (var book in books)
                {
                    Console.WriteLine($"|ID: {book.Id}, |Titulo: {book.Title}, |Autor: {book.Author}, |Categoria: {book.Category}");
                }
            }
            else
            {
                Console.WriteLine("No se encontraron libros.");
            }

            return "";
        }

        private static void LoadBooks()
        {
            // verifica si el objeto JSON existe en la ruta indicada
            if (File.Exists(JsonFilePath))
            {
                // lee el archivo JSON como un archivo de texto
                string json = File.ReadAllText(JsonFilePath);

                try
                {
                    // Intenta deserializar como una lista de libros
                    books = JsonConvert.DeserializeObject<List<Book>>(json);
                }
                catch (JsonSerializationException ex)
                {
                    // Si hay un error, intenta deserializar como un solo objeto Book
                    try
                    {
                        Book singleBook = JsonConvert.DeserializeObject<Book>(json);
                        if (singleBook != null)
                        {
                            books = new List<Book> { singleBook };
                        }
                        else
                        {
                            books = new List<Book>();
                        }
                    }
                    catch (JsonSerializationException innerEx)
                    {
                        Console.WriteLine($"Error al deserializar el archivo JSON como lista de libros: {ex.Message}");
                        Console.WriteLine($"Error al deserializar el archivo JSON como un solo libro: {innerEx.Message}");
                        books = new List<Book>();
                    }
                }
            }
            else
            {
                books = new List<Book>();
            }
        }
        public static string DeleteBook()
        {
            Console.WriteLine("Eliminar Libro");
            Console.WriteLine("Ingrese el ID del libro a eliminar:");
            int id = Convert.ToInt32(Console.ReadLine());

            // Cargar libros existentes del archivo
            LoadBooks();

            // Buscar el libro por ID
            var bookToDelete = books.FirstOrDefault(b => b.Id == id);

            if (bookToDelete == null)
            {
                return $"No se encontró ningún libro con el ID {id}";
            }

            // Eliminar el libro de la lista
            books.Remove(bookToDelete);

            // Guardar la lista actualizada de libros en el archivo JSON
            SaveBooks();

            return $"El libro con ID {id} ha sido eliminado correctamente";
        }

        public static string Exit()
        {
            Console.WriteLine("Saliendo de la aplicación...");
            Environment.Exit(0);
            return "Aplicación cerrada."; // Esto solo se usa por que pide un retorno string
        }

        private static void SaveBooks()
        {
            string json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(JsonFilePath, json);
        }
    }
}
