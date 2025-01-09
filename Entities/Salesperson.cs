namespace MormorDagnysInlämning.Entities;

    public class Salesperson
    {
        public int SalespersonId { get; set; }
        public string SalesRep { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Product Product { get; set; }
        public IList<Product> Products { get; set; }

    }
