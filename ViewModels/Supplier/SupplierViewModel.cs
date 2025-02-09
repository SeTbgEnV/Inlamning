using ViktorEngmanInlämning.ViewModels.Address;

namespace ViktorEngmanInlämning.ViewModels.Supplier;

public class SupplierViewModel : SuppliersViewModel
{
    public IList<AddressViewModel> Addresses { get; set; }
}
