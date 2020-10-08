using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.OrderModels
{
    public class OrderMenuItemDetail
    {
        // Trying this just in the model and controller

        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Menu Item ID")]
        public int MenuItemId { get; set; }

        [Display(Name = "Servings In Stock")]
        public double ServingsInStock { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }
    }
}
