﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Infrastructure.Interfaces
{
    public interface ICartService
    {
        void DecrimentFromCart(int id);

        void IncrimentFromCart(int id);

        void SetQuantityFromCart(int id, int count);

        void RemoveFromCart(int id);

        void AddToCart(int id);

        void RemoveAll();

        CartViewModel TransformCart();
    }
}
