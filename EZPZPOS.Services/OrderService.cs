using EZPZPOS.Data;
using EZPZPOS.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Services
{
    public class OrderService
    {
        private readonly string _userId;

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
                    Notes = model.Notes
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);
                return ctx.SaveChanges() == 1;
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
                                new OrderListItem
                                {
                                    OrderId = e.OrderId,
                                    GuestId = e.GuestId,
                                    FirstName = e.Guest.FirstName,
                                    LastName = e.Guest.LastName,
                                    OrderDateTimeUtc = e.OrderDateTimeUtc,
                                    TypeOfOrder = e.TypeOfOrder,
                                    MenuItemId = e.MenuItemId,
                                    Quantity = e.Quantity,
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
                        OrderDateTimeUtc = entity.OrderDateTimeUtc,
                        TypeOfOrder = entity.TypeOfOrder,
                        MenuItemId = entity.MenuItemId,
                        Name = entity.MenuItem.Name,
                        Quantity = entity.Quantity,
                        Notes = entity.Notes,
                        Gratuity = entity.Gratuity
                    };
            }
        }



    }
}
