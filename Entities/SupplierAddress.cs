using ViktorEngmanInlämning.Entities;

namespace ViktorEngmanInlämning.Entities
{
    public class SupplierAddress
    {
        public int SupplierId { get; set; }
        public int AddressId { get; set; }

        public Salesperson Supplier { get; set; }
        public Address Address { get; set; }
    }
}
