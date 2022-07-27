using ShoppingCartApi.Contracts;
using ShoppingCartApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApi.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly List<ShoppingItem> _shoppingCart;

        public ShoppingCartService()
        {
            _shoppingCart = new List<ShoppingItem>()
            {
                new ShoppingItem() { Id = new Guid("87cc8bdb-d335-460d-95a2-cdca1d7c4ac8"), Name = "Milk", Manufacturer = "Nandini", Price=19},
                new ShoppingItem() { Id = new Guid("e86823c5-0f77-446a-90ba-78bc807f083e"), Name = "Battery", Manufacturer = "Nippon", Price=40},
                new ShoppingItem() { Id = new Guid("28272c83-23d2-4e81-b1e2-d1ff3d78bbc5"), Name = "Sauce", Manufacturer = "Maggi", Price=58},
            };
        }

        public ShoppingItem Add(ShoppingItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            _shoppingCart.Add(newItem);
            return newItem;
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return _shoppingCart;
        }

        public ShoppingItem GetById(Guid id)
        {
            return _shoppingCart.Where(a => a.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var existing = _shoppingCart.First(a => a.Id == id);
            _shoppingCart.Remove(existing);
        }
    }
}
