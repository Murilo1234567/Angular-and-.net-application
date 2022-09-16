using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Service.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseService<Product> _baseProductService;

        public ProductController(IBaseService<Product> baseProductService)
        {
            _baseProductService = baseProductService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductModel Product)
        {
            if (Product == null)
                return NotFound();

            return Execute(() => _baseProductService.Add<ProductModel, ProductValidator>(Product));
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductModel Product)
        {
            if (Product == null)
                return NotFound();

            return Execute(() => _baseProductService.Update<ProductModel, ProductValidator>(Product));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseProductService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseProductService.Get<ProductModel>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseProductService.GetById<ProductModel>(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
