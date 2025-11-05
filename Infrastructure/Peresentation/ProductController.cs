using Microsoft.AspNetCore.Mvc;
using ServicesAbstractionLayer;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PeresentationLayer
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManger _serviceManger):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _serviceManger.productServices.GetAllProductAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManger.productServices.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var brands = await _serviceManger.productServices.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var types = await _serviceManger.productServices.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
