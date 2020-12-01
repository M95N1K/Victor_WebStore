using Victor_WebStore.Domain.DTO.Products;

namespace Victor_WebStore.Domain.DTO.Order
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public ProductDTO Product { get; set; }
    }

}
