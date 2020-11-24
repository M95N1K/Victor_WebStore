﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Victor_WebStore.Clients.Base;
using Victor_WebStore.Domain;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebApiAdress.Products) { }
        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{_ServiceAddress}/brands");

        public IEnumerable<CategoryDTO> GetCategories() => Get<IEnumerable<CategoryDTO>>($"{_ServiceAddress}/category");
        

        public ProductDTO GetProductById(int id)
        {
            return Get<ProductDTO>($"{_ServiceAddress}/{id}");
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter=null) => 
            Post(_ServiceAddress, filter ?? new ProductFilter())
            .Content
            .ReadAsAsync<IEnumerable<ProductDTO>>()
            .Result;
    }
}
