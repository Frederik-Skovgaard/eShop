
namespace WebShop.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }

        public string? ImageUrl { get; set; }

        public int TypesId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<ProductUser>? ProductUsers { get; set; }
        public Types? Types { get; set; }
    }
}
