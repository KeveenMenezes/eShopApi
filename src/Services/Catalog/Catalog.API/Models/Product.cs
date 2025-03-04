namespace Catalog.API.Models;

public class Product(
    Guid id,
    string name,
    string description,
    string imageFile,
    decimal price,
    List<string> categories)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public string ImageFile { get; private set; } = imageFile;
    public decimal Price { get; private set; } = price;
    public List<string> Categories { get; private set; } = categories;

    internal void Update(
        string name,
        string description,
        string imageFile,
        decimal price,
        List<string> categories)
    {
        Name = name;
        Description = description;
        ImageFile = imageFile;
        Price = price;
        Categories = categories;
    }
}
