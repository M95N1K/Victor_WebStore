using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }

    }

}
