using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiendaWeb.Models;

namespace TiendaWeb.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }
        private ProductContext _db = new ProductContext();
        public const string CartSessionKey = "CartId";
        public void AddToCart(int id)
        {
            // Retrive the product from the database.
            ShoppingCartId = GetCartId();
            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);
            if(cartItem == null ) 
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(
                        p => p.ProductId == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.ShoppingCartItems.Add(cartItem);                
            }
            else
            {
                //Si el item existe en el carrito añade uno mas en la cantidad.
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }
        public void Dispose() 
        {
            if(_db != null )
            {
                _db.Dispose();
                _db = null;
            }
        }
        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if(!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    //Generate a new random GUID using system.guid class.
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return _db.ShoppingCartItems.Where(
                c => c.CartId== ShoppingCartId ).ToList();
        }

        public decimal GetTotal()
        {
            ShoppingCartId= GetCartId();
            decimal? total = decimal.Zero; //DataType? nullable type.
            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.CartId == ShoppingCartId
                               select (int?)cartItems.Quantity *
                               cartItems.Product.UnitPrice).Sum();
            return total ?? decimal.Zero; //Coalescing operator tl;dr if the first value is null, use this other value.
        }

        public ShoppingCartActions GetCart(HttpContext context)
        {
            using(var cart = new ShoppingCartActions())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }
        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using(var db = new TiendaWeb.Models.ProductContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach(var cartItem in myCart)
                    {
                        for(int i = 0; i<CartItemCount; i++)
                        {
                            if(cartItem.Product.ProductId == CartItemUpdates[i].ProductId)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.ProductId);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.ProductId, CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("ERROR: Unable to Update Cart DATABASE - " + e.Message.ToString(), e);
                }
            }
        }
        public void RemoveItem(string removeCartID, int removeProductID)
        {
            using(var _db = new TiendaWeb.Models.ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.CartId == removeCartID && c.Product.ProductId == removeProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch(Exception e)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + e.Message.ToString(),e);
                }
            }
        }
        public void UpdateItem(string updateCartID, int updateProductID, int quantity)
        {
            using (var _db = new TiendaWeb.Models.ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.CartId == updateCartID && c.Product.ProductId == updateProductID select c).FirstOrDefault();
                    if(myItem != null)
                    {
                        myItem.Quantity= quantity;
                        _db.SaveChanges();
                    }
                }
                catch(Exception e)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + e.Message.ToString(),e);
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
            _db.SaveChanges();
        }
        
        public int GetCount()
        {
            ShoppingCartId  = GetCartId();

            int? count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            return count ?? 0;
        }

        public struct ShoppingCartUpdates
        {
            public int ProductId;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }
    }
}