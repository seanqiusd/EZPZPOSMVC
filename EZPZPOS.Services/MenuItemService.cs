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
                    ServingsInStock = model.ServingsInStock
                };

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


    }
}
