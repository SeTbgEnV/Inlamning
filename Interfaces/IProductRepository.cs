using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.ViewModels;

namespace ViktorEngmanInlämning.Interfaces;

public interface IProductRepository
{
    public task<List<Product>> Get();
    public Task<Product> Get(int id);
    public Task<Product> Add(Product product);
    public Task<Product> Update(Product product);
    public Task<Product> Delete(int id);
}
