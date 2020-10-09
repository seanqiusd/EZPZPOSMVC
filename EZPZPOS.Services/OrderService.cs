using EZPZPOS.Data;
using EZPZPOS.Models.MenuItemModels;
using EZPZPOS.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EZPZPOS.Services
{
    public class OrderService
    {
        private readonly string _userId;
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public OrderService(string userId)
        {
            _userId = userId;
        }


        //Helper method for OrderController
        public MenuItemDetail OrderMenuItemDetail(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MenuItems
                        .Single(e => e.MenuItemId == id);
                return
                        new MenuItemDetail()
                        {
                            MenuItemId = entity.MenuItemId,
                            ServingsInStock = entity.ServingsInStock,
                            IsAvailable = entity.IsAvailable,
                        };
            }
        }

        // POST -- Create Order
        public bool CreateOrder(OrderCreate model)
        {
            var entity =
                new Order()
                {
                    OrderDateTimeUtc = DateTimeOffset.Now,
                    GuestId = model.GuestId,
                    TypeOfOrder = model.TypeOfOrder,
                    MenuItemId = model.MenuItemId,
                    Quantity = model.Quantity,
                    Notes = model.Notes,

                };

            //Trying 
            //MenuItem item = new MenuItem();
            //var menuItem = item.ServingsInStock -= entity.Quantity;
            //return menuItem;

            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.MenuItems.Single(x => x.MenuItemId == model.MenuItemId);
                if (item.ServingsInStock >= model.Quantity)
                {
                    item.ServingsInStock -= entity.Quantity; // Trying this out to see if it fixes a bug
                }
                if (item.ServingsInStock > 0) // this added && works
                    item.IsAvailable = true; // Trying this
                else
                    item.IsAvailable = false;

                ctx.Orders.Add(entity);
                return ctx.SaveChanges() == 2;
            }
        }

        // GET -- All Orders
        public IEnumerable<OrderListItem> GetOrders()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Orders.ToList()
                        //.Where(e => e.ServerId == _userId)
                        .Select(
                            e =>
                                new OrderListItem()
                                {
                                    OrderId = e.OrderId,
                                    GuestId = e.GuestId,
                                    FirstName = e.Guest.FirstName,
                                    LastName = e.Guest.LastName,
                                    OrderDateTimeUtc = e.OrderDateTimeUtc,
                                    TypeOfOrder = e.TypeOfOrder,
                                    MenuItemId = e.MenuItemId,
                                    Quantity = e.Quantity,
                                    GrandTotal = e.GrandTotal,
                                    Subtotal = e.Subtotal,
                                    Gratuity = e.Gratuity,
                                }

                        );

                return query.ToArray();
            }
        }

        // GET -- Order by ID
        public OrderDetail GetOrderById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderId == id);
                return
                    new OrderDetail
                    {
                        OrderId = entity.OrderId,
                        GuestId = entity.GuestId,
                        FirstName = entity.Guest.FirstName,
                        LastName = entity.Guest.LastName,
                        ContactNumber = entity.Guest.ContactNumber,
                        FullAddress = entity.Guest.FullAddress,
                        OrderDateTimeUtc = entity.OrderDateTimeUtc,
                        TypeOfOrder = entity.TypeOfOrder,
                        MenuItemId = entity.MenuItemId,
                        Name = entity.MenuItem.Name,
                        Quantity = entity.Quantity,
                        Subtotal = entity.Subtotal,
                        Gratuity = entity.Gratuity,
                        SetTax = entity.SetTax,
                        GrandTotal = entity.GrandTotal,
                        Notes = entity.Notes
                    };
            }
        }

        // Update -- Order
        public bool UpdateOrder(OrderEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderId == model.OrderId);

                MenuItem item = ctx.MenuItems.Find(entity.MenuItemId); //change _db to ctx
                if(model.Quantity > entity.Quantity)
                {
                    item.ServingsInStock -= (model.Quantity - entity.Quantity);
                    //subtract difference from items in storage
                    if (item.ServingsInStock > 0)
                        item.IsAvailable = true;
                    else
                        item.IsAvailable = false;
                    ctx.SaveChanges(); //_db.
                }
                if (model.Quantity < entity.Quantity)
                {
                    item.ServingsInStock += (entity.Quantity - model.Quantity);
                    //add the difference back
                    if (item.ServingsInStock > 0)
                        item.IsAvailable = true;
                    else
                        item.IsAvailable = false;
                    ctx.SaveChanges(); //_db.
                }
                entity.TypeOfOrder = model.TypeOfOrder;
                entity.MenuItemId = model.MenuItemId;
                entity.Quantity = model.Quantity;
                entity.Notes = model.Notes;


                // Come back to this later perhaps
                //// Trying Stuff to maybe troubleshoot the bug with editing orders and adding or subtrating the edited amount
                //MenuItem item = ctx.MenuItems.Single(x => x.MenuItemId == model.MenuItemId);
                //if (item.ServingsInStock >= model.Quantity)
                //{

                //    //// find orderId
                //    //var newOrder = _db.Orders.Find(model.OrderId);
                //    //// Find the difference of the second order quantity from the first order quantity since Adding on more
                //    //var addedNewQuantityDifference = model.Quantity -= newOrder.Quantity;
                //    //// -= the difference to the original item.ServingsInStock
                //    //item.ServingsInStock -= addedNewQuantityDifference;
                //    //_db.SaveChanges();


                //    //var newOrder = _db.Orders.Find(model.OrderId);
                //    //MenuItem updatedItem = _db.MenuItems.Find(newOrder.MenuItemId);
                //    ////_db.Orders.Remove(newOrder);

                //    //var updatedOrder = _db.Orders.Find(model.OrderId);
                //    //MenuItem updatedItems = _db.MenuItems.Find(updatedOrder.MenuItemId);
                //    //updatedItems.ServingsInStock -= updatedOrder.Quantity;

                //    //_db.SaveChanges();
                //}
                //else
                //    item.ServingsInStock += entity.Quantity;
                //if (item.ServingsInStock > 0)
                //    item.IsAvailable = true;
                //else
                //    item.IsAvailable = false;
                //// Trying Stuff

                return ctx.SaveChanges() == 1;
            }
        }

        // Delete -- Order
        public bool DeleteOrder(int orderId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderId == orderId);

                ctx.Orders.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
