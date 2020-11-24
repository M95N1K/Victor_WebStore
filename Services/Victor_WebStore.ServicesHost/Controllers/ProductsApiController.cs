﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.ServicesHost.Controllers
{
    [Route(WebApiAddress.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        #region Конструктор
        private readonly IProductService _productService;

        public ProductsApiController(IProductService ProductService)
        {
            _productService = ProductService;
        } 
        #endregion

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands()
        {
            return _productService.GetBrands();
        }
        [HttpGet("category")]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _productService.GetCategories();
        }
        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody]ProductFilter filter=null)
        {
            return _productService.GetProducts(filter);
        }
    }
}
