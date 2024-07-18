using CodigoFacilito.services;
using CodigoFacilito.utils;


while (true)
{
    Console.Clear();
    int? opcion = Options();
    OptionEnum optionEnum = (OptionEnum)opcion;

    if (opcion == 5)
    {
        return;
    }

    string message = optionEnum switch
    {
        OptionEnum.Add => BooksService.AddBook(),
        OptionEnum.Update => BooksService.UpdateBook(),
        OptionEnum.Delete => "Eliminar Libro",
        OptionEnum.Get => BooksService.GetAll(),
        OptionEnum.Exit => "Salir",
        _ => "Opcion no valida"
    };

    Console.WriteLine(message);
    Console.ReadLine();
}
static int? Options()
{
    Console.WriteLine("Bienvenido  a la libreria \n1. Crear Libro \n2. Editar Libro \n3. Eliminar Libro \n4. Obtener listado de Libros \n5. Salir");

    var opcion = Convert.ToInt16(Console.ReadLine());

    return opcion;
}