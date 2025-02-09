using ViktorEngmanInlämning.Entities;

namespace ViktorEngmanInlämning.Entities;

public class SupplierProduct
{
    public int ProductId { get; set; }
    public int SalespersonId { get; set; }
    public string ItemNumber { get; set; }
    public decimal Price { get; set; }

    public Product Product { get; set; }
    public Salesperson Supplier { get; set; }
}
