namespace Tema_1.Catalog;

public class ProductFilter
{
    public string? Category { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public ProductOrderBy? OrderBy { get; set; }

    public bool Descending { get; set; }
}

public enum ProductOrderBy
{
    Name,
    Price
}