﻿using EZPZPOS.Data;
using EZPZPOS.Models.OrderModels;
using System;
using System.Collections.Generic;
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
                MenuItem item = ctx.MenuItems.Single(x => x.MenuItemId == model.MenuItemId);
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

                entity.TypeOfOrder = model.TypeOfOrder;
                entity.MenuItemId = model.MenuItemId;
                entity.Quantity = model.Quantity;
                entity.Notes = model.Notes;

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
