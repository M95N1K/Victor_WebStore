using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.ViewModels
{
    public class BrandViewModel : INameEntity, ICountEntity, IOrderEtinty
    {
        public string Name { get ; set ; }
        public int Id { get ; set ; }
        public int Count { get ; set ; }
        public int Order { get ; set ; }
    }
}
