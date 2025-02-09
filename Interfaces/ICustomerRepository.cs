using ViktorEngmanInlämning.ViewModels.Customer;

namespace ViktorEngmanInlämning.Interfaces;

public interface ICustomerRepository
{
    public Task<IList<CustomersViewModel>> List();
    public Task<CustomerViewModel> Find(int id);
    public Task<bool> Add(CustomerPostViewModel model);
}
