using Core.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CoreSeeder
    {
        private readonly CoreContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public CoreSeeder(CoreContext ctx,IHostingEnvironment hosting,UserManager<StoreUser> userManager)
        {
            this._ctx = ctx;
            this._hosting = hosting;
            this._userManager = userManager;
        }
        public async Task Seed()
        {
            //check created database
            _ctx.Database.EnsureCreated();

            var user =await  _userManager.FindByEmailAsync("docaotri211997@gmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Tri",
                    LastName = "Do",
                    UserName="docaotri211997@gmail.com",
                    Email= "docaotri211997@gmail.com"
                };
                var result = await _userManager.CreateAsync(user,"P@assw0rd!");
                if(result!= IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            };

            //if return >0 =>true
            if (!_ctx.Products.Any())
            {
                //need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath,"Models/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User=user,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    }
                   // Items = null
                };
                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}
