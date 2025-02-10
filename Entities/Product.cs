namespace ViktorEngmanInlämning.Entities;

public class Product
{
    public int Id { get; set; }
    public string ItemNumber { get; set; }
    public string ProductName { get; set; }
    public double KgPrice { get; set; }
    public string ImageURL { get; set; }
    public string Description { get; set; }
    public int SalesRepId { get; set; }
    public IList<OrderItem> OrderItems { get; set; }
    public SupplierProduct SupplierProduct { get; set; }
}
