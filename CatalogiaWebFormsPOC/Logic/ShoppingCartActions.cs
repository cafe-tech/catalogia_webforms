using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CatalogiaWebForms.Models;

namespace CatalogiaWebForms.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }
        private CatalogObjectContext _db = new CatalogObjectContext();
        public const string CartSessionKey = "CartId";

        public void AddToCart(string ItemId)
        {
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(c => c.CartId == ShoppingCartId && c.CatalogObject.ObjectId == ItemId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),                    
                    CartId = ShoppingCartId,
                    CatalogObject = _db.CatalogObjects.SingleOrDefault(
                        p => p.ObjectId == ItemId),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    HttpContext.Current.Session[CartSessionKey] = Guid.NewGuid().ToString();
                }
            }

            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.ShoppingCartItems.Where(x => x.CartId == ShoppingCartId).ToList();
        }

        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();

            decimal? totals = decimal.Zero;
            
            totals = (decimal?) (from cartItems in _db.ShoppingCartItems
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.Quantity * cartItems.CatalogObject.Price).Sum();           

            return totals ?? decimal.Zero;
        }

        public ShoppingCartActions GetCart(HttpContext context)
        {
            using (var cart = new ShoppingCartActions())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }

        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using (var db = new CatalogiaWebForms.Models.CatalogObjectContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        // Iterate through all rows within shopping cart list
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.CatalogObject.ObjectId == CartItemUpdates[i].ObjectId)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.CatalogObject.ObjectId);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.CatalogObject.ObjectId, CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void RemoveItem(string removeCartID, string objectIdToRemove)
        {
            using (var _db = new CatalogiaWebForms.Models.CatalogObjectContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.CartId == removeCartID && c.CatalogObject.ObjectId == objectIdToRemove select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void UpdateItem(string updateCartID, string updateObjectId, int quantity)
        {
            using (var _db = new CatalogiaWebForms.Models.CatalogObjectContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.CartId == updateCartID && c.CatalogObject.ObjectId == updateObjectId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void EmptyCart()
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }
            // Save changes.             
            _db.SaveChanges();
        }

        public int GetCount()
        {
            ShoppingCartId = GetCartId();

            // Get the count of each item in the cart and sum them up          
            int? count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null         
            return count ?? 0;
        }

        public struct ShoppingCartUpdates
        {
            public string ObjectId;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }

        public void MigrateCart(string cartId, string userName)
        {
            var shoppingCart = _db.ShoppingCartItems.Where(c => c.CartId == cartId);
            foreach (CartItem item in shoppingCart)
            {
                item.CartId = userName;
            }
            HttpContext.Current.Session[CartSessionKey] = userName;
            _db.SaveChanges();
        }
    }
}