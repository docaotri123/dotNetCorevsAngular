using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CoreRepository : ICoreRepository
    {
        private readonly CoreContext _ctx;
        private readonly ILogger<CoreRepository> _logger;

        public CoreRepository(CoreContext ctx,ILogger<CoreRepository> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }

        public void AddEntity(Order model)
        {
            _ctx.Orders.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            //return _ctx.Orders.ToList();
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username)
        {
            return _ctx.Orders
                .Where(o=>o.User.UserName==username)
               .Include(o => o.Items)
               .ThenInclude(i => i.Product)
               .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("Get All Products");
            return _ctx.Products
                .OrderBy(p => p.Title)
                .Take(8)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders.Where(i => i.Id.Equals(id)).FirstOrDefault();
        }

        public Order GetOrderById(string name, int orderId)
        {
            return _ctx.Orders
                .Where(i => i.Id.Equals(orderId)&&i.User.UserName==name)
                .Include(o=>o.Items)
                .ThenInclude(i=>i.Product)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category.Equals(category))
                .ToList();
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
