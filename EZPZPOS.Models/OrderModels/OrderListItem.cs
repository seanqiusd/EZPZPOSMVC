using EZPZPOS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.OrderModels
{
    public class OrderListItem
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Guest ID")]
        public int GuestId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Order Date/Time")]
        public DateTimeOffset OrderDateTimeUtc { get; set; }

        [Display(Name = "Order Type")]
        public OrderType TypeOfOrder { get; set; }

        [Display(Name = "Menu Item Id")]
        public int MenuItemId { get; set; }

        [Display(Name = "Quantity Needed")]
        [Range(1, 10000, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public decimal Subtotal { get; }

        [Display(Name = "Tip Amount")]
        public decimal Gratuity { get; set; }

        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; }
    }
}
