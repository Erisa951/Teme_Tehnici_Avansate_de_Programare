namespace Tema3.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> SearchByNameAsync(string name);
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}