using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Entities;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace Core.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly ICoreRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(ICoreRepository repository,ILogger<OrdersController> logger,
            IMapper mapper,
            UserManager<StoreUser> userManager
            )
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var username = User.Identity.Name;
                var list = _mapper.Map<List<OrderViewModel>>(_repository.GetAllOrdersByUser(username));
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if(order!=null)
                    return Ok(_mapper.Map<OrderViewModel>(order));
                return NotFound("Khong Thay");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpPost]
        public async Task< IActionResult> Post([FromBody]OrderViewModel model)
        {
            //add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order()
                    {
                        OrderDate = model.OrderDate,
                        OrderNumber=model.OrderNumber,
                        Id=model.OrderId
                    };
                    var currentUser =await  _userManager.FindByNameAsync(User.Identity.Name) ;
                    newOrder.User = null;
                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/order/{newOrder.Id}", model); //201
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to save a new order:{ex}");
               
            }
            return BadRequest("Failed to save");
        }

        private IActionResult Created(Order model)
        {
            throw new NotImplementedException();
        }
    }
}