namespace MiniShop.Entity
{
    public class Product: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double NormalPrice { get; set; }
        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsOnSale => Price != NormalPrice;
    }
}