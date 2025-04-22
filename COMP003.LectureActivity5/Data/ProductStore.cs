using COMP003.LectureActivity5.Models;

namespace COMP003.LectureActivity5.Data;

public static class ProductStore
{
    public static List<Product> Products { get; } = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 1200.00m },
        new Product { Id = 2, Name = "Mouse", Price = 25.99m },
        new Product { Id = 3, Name = "Keyboard", Price = 49.99m }
    };
}