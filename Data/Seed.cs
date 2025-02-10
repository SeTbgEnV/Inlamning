using System.Text.Json;
using ViktorEngmanInlämning.Data;
using ViktorEngmanInlämning.Entities;

namespace ViktorEngmanInlämning.Data.Migrations;

public class Seed
{
    public static async Task LoadProducts(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        if (context.Products.Any())
        {
            Console.WriteLine("Products already loaded");
            return;
        }
        else
        {
            Console.WriteLine("Products not loaded");
        }
        var json = File.ReadAllText("Data/json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);
        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadSalesOrders(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        if (context.SalesOrders.Any())
        {
            Console.WriteLine("SalesOrders already loaded");
            return;
        }
        else
        {
            Console.WriteLine("SalesOrders not loaded");
        }

        var json = File.ReadAllText("Data/json/orders.json");
        var salesOrders = JsonSerializer.Deserialize<List<SalesOrder>>(json, options);
        if (salesOrders is not null && salesOrders.Count > 0)
        {
            await context.SalesOrders.AddRangeAsync(salesOrders);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadSalesPeople(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        if (context.Salespeople.Any())
        {
            Console.WriteLine("Salespeople already loaded");
            return;
        }
        else
        {
            Console.WriteLine("Salespeople not loaded");
        }
        var json = File.ReadAllText("Data/json/salespeople.json");
        var salesPeople = JsonSerializer.Deserialize<List<Supplier>>(json, options);
        if (salesPeople is not null && salesPeople.Count > 0)
        {
            await context.Salespeople.AddRangeAsync(salesPeople);
            await context.SaveChangesAsync();
        }
    }

    public static async Task LoadOrderItems(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        if (context.OrderItems.Any())
        {
            Console.WriteLine("OrderItems already loaded");
            return;
        }
        else
        {
            Console.WriteLine("OrderItems not loaded");
        }
        var json = File.ReadAllText("Data/json/orderitems.json");
        var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(json, options);
        if (orderItems is not null && orderItems.Count > 0)
        {
            await context.OrderItems.AddRangeAsync(orderItems);
            await context.SaveChangesAsync();
        }
    }
}
