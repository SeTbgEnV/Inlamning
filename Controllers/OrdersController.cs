using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViktorEngmanInlämning.Data;
using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.ViewModels;

namespace MormorDagnysInlämning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SalesSupport, Admin")]
    public class OrdersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult> ListAllOrders()
        {
            var orders = await _context.SalesOrders
                .Include(SalesOrder => SalesOrder.OrderItems)
                .Select(order => new
                {
                    Ordernumber = order.SalesOrderId,
                    order.OrderDate,
                    Orderitems = order.OrderItems
                        .Select(item => new
                        {
                            item.Product.ProductName,
                            item.Price,
                            item.Quantity,
                            Linesum = item.Price * item.Quantity
                        })
                })
                .ToListAsync();
            return Ok(new { success = true, StatusCode = 200, data = orders });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            var order = await _context.SalesOrders
                .Where(SalesOrder => SalesOrder.SalesOrderId == id)
                .Include(SalesOrder => SalesOrder.OrderItems)
                .Select(order => new
                {
                    Ordernumber = order.SalesOrderId,
                    order.OrderDate,
                    Orderitems = order.OrderItems
                        .Select(item => new
                        {
                            item.Product.ProductName,
                            item.Price,
                            item.Quantity,
                            Linesum = item.Price * item.Quantity
                        })
                })
                .SingleOrDefaultAsync();

            if (order == null)
            {
                return BadRequest (new { success = false, StatusCode = 400, message = $"Order with id {id} not found" });
            }
            return Ok(new { success = true, StatusCode = 200, data = order });
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(SalesOrderViewModel order)
        {
            var newOrder = new SalesOrder
            {
                OrderDate = order.OrderDate,
                OrderItems = new List<OrderItem>()
            };

            foreach (var product in order.Products)
            {
                var p = await _context.Products.SingleOrDefaultAsync(p => p.ProductName.ToLower() == product.ProductName.ToLower());
                if (p != null)
                {
                    var item = new OrderItem
                    {
                        Product = p,
                        Quantity = product.Quantity,
                        Price = product.Price
                    };
                    newOrder.OrderItems.Add(item);
                }
            }

            try
            {
                await _context.SalesOrders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.SalesOrderId }, new
                {
                    newOrder.SalesOrderId,
                    newOrder.OrderDate,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, StatusCode = 400, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, SalesOrderViewModel order)
        {
            var orderToUpdate = await _context.SalesOrders
                .Where(SalesOrder => SalesOrder.SalesOrderId == id)
                .Include(SalesOrder => SalesOrder.OrderItems)
                .SingleOrDefaultAsync();

            if (orderToUpdate == null)
            {
                return BadRequest(new { success = false, StatusCode = 404, message = $"Order with id {id} not found" });
            }

            orderToUpdate.OrderDate = order.OrderDate;
            foreach (var item in order.Products)
            {
                var orderItem = orderToUpdate.OrderItems.FirstOrDefault(oi => oi.Product.ProductName.ToLower() == item.ProductName.ToLower());
                if (orderItem != null)
                {
                    orderItem.Quantity = item.Quantity;
                    orderItem.Price = item.Price;
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, StatusCode = 400, message = ex.Message });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var orderToDelete = await _context.SalesOrders.FindAsync(id);

            if (orderToDelete == null)
            {
                return BadRequest(new { success = false, StatusCode = 404, message = $"Order with id {id} not found" });
            }

            _context.SalesOrders.Remove(orderToDelete);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, StatusCode = 400, message = ex.Message });
            }

            return NoContent();
        }
    }
}