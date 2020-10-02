using EZPZPOS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.OrderModels
{
    public class OrderEdit
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Order Type")]
        public OrderType TypeOfOrder { get; set; }

        [Required]
        [Display(Name = "Menu Item ID")]
        public int MenuItemId { get; set; }

        [Required]
        [Display(Name = "Quantity Needed")]
        [Range(1, 10000, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Display(Name = "Special Instructions")]
        public string Notes { get; set; }


    }
}
