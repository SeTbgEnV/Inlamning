using ViktorEngmanInlämning.ViewModels.Address;

namespace ViktorEngmanInlämning.ViewModels.Customer;

public class CustomerPostViewModel : CustomerBaseViewModel
{
      public IList<AddressPostViewModel> Addresses { get; set; }
}
