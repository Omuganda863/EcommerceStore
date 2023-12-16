using AutoMapper;
using EcommerceStore.DTOs;
using EcommerceStore.Models;
using EcommerceStore.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Iproducts _products;
        private readonly ResponseDTO responseDTO = new ResponseDTO();
        public ProductsController(IMapper mapper, Iproducts products)
        {
            _mapper = mapper;
            _products = products;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllProducts()
        {
            var Prducts = await _products.GetAllProducts();
            responseDTO.Message = "something";
            responseDTO.Result = Prducts;
            return Ok(responseDTO);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddProduct(AddProductsDTO product)
        {

            var NewProduct = _mapper.Map<Product>(product);
            var ReturnPrduct = await _products.CreateProduct(NewProduct);
            responseDTO.Result = ReturnPrduct;
            responseDTO.Message = "Product Created Successfully";
            return Ok(responseDTO);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetProduct(Guid id)
        {
            var prdct = await _products.GetProduct(id);
            if (prdct == null)
            {
                responseDTO.Message = "Not found";
                responseDTO.StatusCode= System.Net.HttpStatusCode.NotFound;
                return NotFound(responseDTO);  
            }
            responseDTO.Result= prdct;
            return Ok(responseDTO);
           
        }


    }
}
