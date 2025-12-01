using MiniProjects.Frontend.Models; // ใช้ Namespace ที่ถูกต้อง

namespace MiniProjects.Frontend.Services // ใช้ Namespace ที่ถูกต้อง
{
    // Interface สำหรับการลงทะเบียน Service
    public interface ICartService
    {
        event Action? OnChange; // Event สำหรับแจ้งเตือน Component เมื่อตะกร้ามีการเปลี่ยนแปลง
        List<CartItem> GetItems();
        void AddItem(Product product);
        void RemoveItem(int productId);
        void ClearCart();
        decimal GetTotalAmount();
    }

    // Class ที่ทำหน้าที่จัดการ Logic ตะกร้าสินค้า
    public class CartService : ICartService
    {
        // เก็บสถานะตะกร้าสินค้าในหน่วยความจำ
        private readonly List<CartItem> _cartItems = new List<CartItem>();

        public event Action? OnChange;

        public List<CartItem> GetItems() => _cartItems;

        public void AddItem(Product products)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.ProductId == products.ProductId);

            if (existingItem != null)
            {
                // ถ้ามีสินค้าอยู่แล้ว ให้อัปเดตจำนวน
                existingItem.Quantity++;
            }
            else
            {
                // ถ้าเป็นสินค้าใหม่ ให้เพิ่มเข้าไปในตะกร้า
                _cartItems.Add(new CartItem
                {
                    ProductId = products.ProductId,
                    Name = products.Name,
                    Price = products.Price,
                    Quantity = 1
                });
            }

            NotifyStateChanged();
        }


        public void RemoveItem(int productId)
        {
            var itemToRemove = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                _cartItems.Remove(itemToRemove);
                NotifyStateChanged();
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
            NotifyStateChanged();
        }

        public decimal GetTotalAmount()
        {
            return _cartItems.Sum(i => i.TotalPrice);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}