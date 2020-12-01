using System.Collections.Generic;

namespace Victor_WebStore.Domain.DTO.Order
{
    public class CreateOrderModel
    { 
        public OrderDTO Order { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }

}
