using ViktorEngmanInlämning.ViewModels.Address;

namespace ViktorEngmanInlämning.ViewModels.Customer;

public class CustomerViewModel : CustomerBaseViewModel
{
    public int Id { get; set; }
    public IList<AddressViewModel> Addresses { get; set; }
}
