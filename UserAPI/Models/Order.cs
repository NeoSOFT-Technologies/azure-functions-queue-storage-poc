namespace UserAPI.Models
{
    public class Order
    {
        public int UserId { get; set; }
        public string StockName { get; set; }
        public int Quantity { get; set; }
    }
}
