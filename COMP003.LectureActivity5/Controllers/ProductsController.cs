using COMP003.LectureActivity5.Data;
using COMP003.LectureActivity5.Models;
using Microsoft.AspNetCore.Mvc;
namespace COMP003.LectureActivity5.Controllers;

//This controller handles product-related API requests.
[ApiController]
[Route("[controller]")]
public class ProductsController : Controller
{
    // GET: api/products
    // This endpoint retrieves all products.
    [HttpGet]
    public ActionResult<List<Product>> GetProducts()
    {
        // returns all products from the ProductStore
        return Ok(ProductStore.Products);
    }
    
    // GET: api/products/{id}
    // This endpoint retrieves a specific product by its ID
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        // Find the product with the specified ID
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        
        // if the product is not found, return a 404 Not Found response
        if (product is null)
            return NotFound();
        
        // if the product is found, return it with a 200 OK response
        return Ok(product);
    }
    
    // POST: api/products
    // This endpoint creates a new product.
    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        // Find the maximum ID in the existing products and assign a new ID (max+1) to the new product
        product.Id = ProductStore.Products.Max(p => p.Id) + 1;
        ProductStore.Products.Add(product);
        
        // Return a 201 Created response with the location of the new product
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id}, product);
    }
    
    // PUT: api/products/{id}
    // This endpoint updates an existing product.
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product updatedProduct)
    {
        // Check if the product with the specified ID exists
        var existingProduct = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        
        // If the product is not found, return a 404 Not Found Response
        if (existingProduct is null)
            return NotFound();
        
        //Update the existing product's properties with the new values
        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        
        // Return a 204 No Content response to indicate success
        return NoContent();
    }
    
    // DELETE: api/products/{id}
    // This endpoint deletes a product by its ID.
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        // Find the product with the specified ID
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        
        // if the product is not found, return a 404 Not Found response
        if (product is null)
            return NotFound();
        
        // Remove the product from the ProductStore
        ProductStore.Products.Remove(product);
        
        // Return a 204 No Content response to indicate success
        return NoContent();
    }
    
    // GET: api/products/filter?price={price}
    // This endpoint retrieves products filtered by price.
    [HttpGet("filter")]
    public ActionResult<List<Product>> FilterProducts(decimal price)
    {
        // Filter products based on the specified price and order them by price
        var filteredProducts = ProductStore.Products
            .Where(p => p.Price <= price)
            .OrderBy(p => p.Price)
            .ToList();
        
        // if no products match the filter, return a 404 Not Found response
        return Ok(filteredProducts);
    }
    
    // GET: api/products/names
    [HttpGet("names")]
    public ActionResult<List<string>> GetProductNames()
    {
        // Retrieve product names and order them alphabetically
        var productNames = ProductStore.Products
            .OrderBy(p => p.Name)
            .Select(p => p.Name)
            .ToList();
        
        // if no product names are found, return a 404 Not Found response
        return Ok(productNames);
    }
}