using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.MenuItemModels
{
    public class MenuItemListItem
    {
        [Display(Name= "Menu Item ID")]
        public int MenuItemId { get; set; }

        [Display(Name = "Menu Item Name")]
        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

    }
}
