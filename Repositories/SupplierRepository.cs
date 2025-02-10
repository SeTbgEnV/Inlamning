using Microsoft.EntityFrameworkCore;
using ViktorEngmanInlämning.ViewModels.Supplier;
using ViktorEngmanInlämning.Data;
using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.Helpers;
using ViktorEngmanInlämning.Interfaces;
using ViktorEngmanInlämning.ViewModels.Address;

namespace MormorDagnysInlämning.Repositories;

public class SupplierRepository(DataContext context, IAddressRepository repo) : ISupplierRepository
{
  private readonly DataContext _context = context;
  private readonly IAddressRepository _repo = repo;

  public async Task<bool> Add(SupplierPostViewModel model)
  {
    try
    {
      if (await _context.Salespeople.FirstOrDefaultAsync(c => c.Email.ToLower().Trim() ==
       model.Email.ToLower().Trim()) != null)
      {
        throw new DagnysException("Leverantören finns redan");
      }

      var supplier = new Supplier
      {
        CompanyName = model.CompanyName,
        Email = model.Email,
        PhoneNumber = model.Phone
      };

      await _context.Salespeople.AddAsync(supplier);

      foreach (var a in model.Addresses)
      {
        var address = await _repo.Add(a);
        await _context.SupplierAddresses.AddAsync(new SupplierAddress { Supplier = supplier, Address = address });
      }

      return await _context.SaveChangesAsync() > 0;
    }
    catch (DagnysException ex)
    {
      throw new Exception(ex.Message);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }

  }

  public async Task<SupplierViewModel> GetSupplier(int id)
  {
    try
    {
      var supplier = await _context.Salespeople
      .Where(s => s.Id == id)
      .Include(s => s.SupplierAddresses)
        .ThenInclude(s => s.Address)
        .ThenInclude(s => s.PostalAddress)
      .Include(s => s.SupplierAddresses)
        .ThenInclude(s => s.Address)
        .ThenInclude(s => s.AddressType)
      .SingleOrDefaultAsync();

      // Om inte leverantören finns så är det kört här...
      if (supplier is null)
      {
        throw new DagnysException($"Finns ingen leverantör med id {id}");
      }

      var view = new SupplierViewModel
      {
        SalespersonId = supplier.Id,
        CompanyName = supplier.CompanyName,
        Email = supplier.Email,
        Phone = supplier.PhoneNumber
      };

      IList<AddressViewModel> addresses = [];

      foreach (var a in supplier.SupplierAddresses)
      {
        var addressView = new AddressViewModel
        {
          AddressLine = a.Address.AddressLine,
          PostalCode = a.Address.PostalAddress.PostalCode,
          City = a.Address.PostalAddress.City,
          AddressType = a.Address.AddressType.Value
        };

        addresses.Add(addressView);
      }

      view.Addresses = addresses;

      return view;
    }
    catch (DagnysException ex)
    {
      throw new Exception(ex.Message);
    }
    catch (Exception ex)
    {
      throw new Exception($"Hoppsan det gick fel {ex.Message}");
    }
  }

  public async Task<IList<SuppliersViewModel>> List()
  {
    try
    {
      var suppliers = await _context.Salespeople.ToListAsync();

      IList<SuppliersViewModel> response = [];

      foreach (var supplier in suppliers)
      {
        var view = new SuppliersViewModel
        {
          SalespersonId = supplier.Id,
          CompanyName = supplier.CompanyName,
          Email = supplier.Email,
          Phone = supplier.PhoneNumber
        };

        response.Add(view);
      }

      return response;
    }
    catch (Exception ex)
    {
      throw new Exception($"Ett fel inträffade i ListAllSupplier(SupplierRepository) {ex.Message}");
    }

  }

}
