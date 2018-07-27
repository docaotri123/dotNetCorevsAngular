using System.Collections.Generic;
using Core.Models.Entities;

namespace Core.Models
{
    public interface ICoreRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void AddEntity(Order model);
        IEnumerable<Order> GetAllOrdersByUser(string username);
        Order GetOrderById(string name, int orderId);
    }
}