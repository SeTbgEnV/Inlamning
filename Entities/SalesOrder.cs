namespace MormorDagnysInlämning.Entities;

    public class SalesOrder
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
