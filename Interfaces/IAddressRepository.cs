using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.ViewModels.Address;

namespace ViktorEngmanInlämning.Interfaces;

public interface IAddressRepository
{
    public Task<Address> Add(AddressPostViewModel model);
}
