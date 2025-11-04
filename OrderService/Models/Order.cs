namespace OrderService.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal TotalValue => Items.Sum(item => item.Price);
        public string Status { get; set; } = "Placed";
        public List<FoodItem> Items { get; set; } = new();
    }
}
