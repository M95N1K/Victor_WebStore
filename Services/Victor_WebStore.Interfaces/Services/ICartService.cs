using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Interfaces.Services
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
