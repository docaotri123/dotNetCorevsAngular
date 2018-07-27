using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly ICoreRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ICoreRepository repository,ILogger<ProductsController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Failed to log products:{ex}");
                return BadRequest("Failed to log products");
            }     
        }
       
       
    }
}