﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.ViewModels
{
    public class CategoryViewModel : INameEntity, IOrderEtinty
    {
        public CategoryViewModel()
        {
            ChildCategories = new List<CategoryViewModel>();
        }

        public int Order { get ; set ; }
        public string Name { get ; set ; }
        public int Id { get ; set ; }

        /// <summary>
        /// Дочерние секции
        /// </summary>
        public List<CategoryViewModel> ChildCategories { get; set; }

        /// <summary>
        /// Родительская секция
        /// </summary>
        public CategoryViewModel ParentCategory { get; set; }
    }
}
