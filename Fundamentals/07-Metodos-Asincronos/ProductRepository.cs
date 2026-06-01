using CsBases.Fundamentals;

public class ProductRepository
{
    public async Task<Product> GetProduct(int id)
    {
        // Obtener de una base de datos, 
        // una llamada de API externa o 
        // podría ser un archivo.
        WriteLine("Buscando producto...");
        await Task.Delay(2000);
        return new Product("Computadora", 500);
    }
}