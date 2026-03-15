using System.Linq;

namespace Tema_1.Catalog
{
    public class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal GetTotalPrice()
        {
            return Enumerable
                .Repeat(Product.Price, Quantity)
                .Sum(price => price);
        }
    }
}