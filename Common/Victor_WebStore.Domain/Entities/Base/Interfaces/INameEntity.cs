using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain.Entities.Base.Interfaces
{
    public interface INameEntity : IBaseEntity
    {
        string Name { get; set; }
    }
}
