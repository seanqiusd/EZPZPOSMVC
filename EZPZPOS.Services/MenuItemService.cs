using EZPZPOS.Data;
using EZPZPOS.Models.MenuItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Services
{
    public class MenuItemService
    {
        private readonly string _userId;

        public MenuItemService(string userId)
        {
            _userId = userId;
        }

        // POST -- Create Menu Item
        public bool CreateMenuItem(MenuItemCreate model)
        {
            var entity =
                new MenuItem()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Category = model.Category,
                    Price = model.Price,
                    ServingsInStock = model.ServingsInStock,
                    IsAvailable = false
                };
            if (entity.ServingsInStock > 0) // Just added all this
                entity.IsAvailable = true;
            else
                entity.IsAvailable = false;


            using (var ctx = new ApplicationDbContext())
            {
                ctx.MenuItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // GET -- All MenuItems
        public IEnumerable<MenuItemListItem> GetMenuItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .MenuItems.ToList()
                        //.Where(e => e.ServerId == _userId)
                        .Select(
                            e =>
                                new MenuItemListItem
                                {
                                    MenuItemId = e.MenuItemId,
                                    Name = e.Name,
                                    Category = e.Category,
                                    Price = e.Price,
                                    IsAvailable = e.IsAvailable,
                                }

                        );

                return query.ToArray();
            }
        }

        // GET -- Menu Item by ID
        public MenuItemDetail GetMenuItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MenuItems
                        .Single(e => e.MenuItemId == id);
                return
                    new MenuItemDetail
                    {
                        MenuItemId = entity.MenuItemId,
                        Name = entity.Name,
                        Description = entity.Description,
                        Category = entity.Category,
                        Price = entity.Price,
                        ServingsInStock = entity.ServingsInStock
                    };
            }
        }

        // Update -- Menu Item
        public bool UpdateMenuItem(MenuItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MenuItems
                        .Single(e => e.MenuItemId == model.MenuItemId);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Category =model.Category;
                entity.Price = model.Price;
                entity.ServingsInStock = model.ServingsInStock;

                // Just added this
                if (entity.ServingsInStock > 0)
                    entity.IsAvailable = true;
                else
                    entity.IsAvailable = false;

                return ctx.SaveChanges() == 1;
            }
        }

        // Delete -- MenuItem
        public bool DeleteMenuItem(int menuItemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MenuItems
                        .Single(e => e.MenuItemId == menuItemId);

                ctx.MenuItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }



    }
}
