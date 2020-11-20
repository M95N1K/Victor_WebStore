using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get ; set; }
    }
}
