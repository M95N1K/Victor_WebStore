﻿using System;
using System.Collections.ObjectModel;
using Victor_WebStore.Domain.Entities.Base;

namespace Victor_WebStore.Domain.Entities
{
    public class Order : NamedEntity
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }

        public virtual Collection<OrderItem> OrderItems { get; set; }
    }
}