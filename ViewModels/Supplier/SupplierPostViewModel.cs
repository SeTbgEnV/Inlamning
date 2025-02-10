using ViktorEngmanInlämning.ViewModels;

namespace ViktorEngmanInlämning.ViewModels.Supplier;

public class SupplierPostViewModel
{
    public string CompanyName { get; set; }
    public string SalesRep { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public IList<AddressPostViewModel> Addresses { get; set; }
}
