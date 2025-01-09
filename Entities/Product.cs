namespace MormorDagnysInlämning.Entities;

    public class Product
    {
        public int ProductId { get; set; }
        public string ItemNumber { get; set; }
        public string ProductName { get; set; }
        public double KgPrice { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public int SalesRepId { get; set; }
        public Salesperson Salesperson { get; set; }
    }
