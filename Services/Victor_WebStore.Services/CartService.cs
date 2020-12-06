using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.Services
{
    public class CartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly ICartStore _cartStore;

        public CartService(IProductService productService, ICartStore cartStore)
        {
            _productService = productService;
            _cartStore = cartStore;
        }

        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });

            _cartStore.Cart = cart;
        }

        public void DecrimentFromCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
                return;

            if (item.Quantity > 1)
                item.Quantity--;

            _cartStore.Cart = cart;
        }

        public void IncrimentFromCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
                return;

            item.Quantity++;

            _cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = _cartStore.Cart;
            cart.Items.Clear();
            _cartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;


            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
                return;

            cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductFilter
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()
            }).Select(p => new ProductViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                Brand = p.Brand != null ? p.Brand.Name : string.Empty
            }).ToList();

            var r = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(
                    x => products.First(y => y.Id == x.ProductId),
                    x => x.Quantity)
            };

            return r;
        }

        public IEnumerable<OrderItemDTO> ToOrderItems()
        {
            var products = _productService.GetProducts(null);
            List<OrderItemDTO> result = new List<OrderItemDTO>();
            foreach (var item in _cartStore.Cart.Items)
            {
                result.Add( new OrderItemDTO
                {
                    Price = products.FirstOrDefault(p => p.Id == item.ProductId).Price,
                    Quantity = item.Quantity,
                    Product = products.FirstOrDefault(p => p.Id == item.ProductId)
                });
            }

            return result;
        }


        public void SetQuantityFromCart(int id, int count)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
                return;

            if (count > 0)
                item.Quantity = count;

            _cartStore.Cart = cart;
        }
    }
}
