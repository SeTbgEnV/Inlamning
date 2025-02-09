using ViktorEngmanInlämning.ViewModels.Supplier;

namespace ViktorEngmanInlämning.Interfaces;

public interface ISupplierRepository
{
    public Task<IList<SuppliersViewModel>> ListAllSuppliers();
    public Task<SupplierViewModel> GetSupplier(int id);
    public Task<bool> Add(SupplierPostViewModel model);
}
