namespace Tema3.Products;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task AddProductAsync(string name, decimal price, int stock, string category)
    {
        var product = new Product(name, price, stock, category);
        await _productRepository.AddAsync(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string name)
    {
        return await _productRepository.SearchByNameAsync(name);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
    {
        return await _productRepository.GetByCategoryAsync(category);
    }

    public async Task UpdatePriceAsync(int productId, decimal newPrice)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            throw new InvalidOperationException("Product not found.");

        product.UpdatePrice(newPrice);
        await _productRepository.UpdateAsync(product);
    }
}