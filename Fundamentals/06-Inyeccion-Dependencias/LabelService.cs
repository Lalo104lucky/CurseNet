using CsBases.Fundamentals;

public class LabelService : ILabelService
{
    public string GenerateLabel(Product product)
    {
        return $"{product.Name} - Precio: {product.Price:C} - Código {product.GetType().Name}-{product.GetHashCode()}";
    }
}