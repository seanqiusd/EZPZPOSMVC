using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.MenuItemModels
{
    public class MenuItemDetail
    {
        [Display(Name = "Menu Item ID")]
        public int MenuItemId { get; set; }

        [Display(Name = "Menu Item Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Servings In Stock")]
        public double ServingsInStock { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }


    }
}
