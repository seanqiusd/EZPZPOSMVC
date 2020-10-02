using EZPZPOS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.OrderModels
{
    public class OrderDetail
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Guest ID")]
        public int GuestId { get; set; }

        [Display(Name = "Guest Name")]
        public string FullName { get; }

        [Display(Name = "Order Date/Time")]
        public DateTimeOffset OrderDateTimeUtc { get; set; }

        [Display(Name = "Order Type")]
        public OrderType TypeOfOrder { get; set; }

        [Display(Name = "Menu Item ID")]
        public int MenuItemId { get; set; }

        [Display(Name = "Menu Item Name")]
        public string Name { get; set; }

        [Display(Name = "Quantity Needed")]
        public int Quantity { get; set; }

        public string Notes { get; set; }


        [Display(Name = "Tax Rate")]
        public decimal SetTax
        {
            get
            {
                decimal taxRate = 0.07m;
                return taxRate;
            }
        }

        public decimal Subtotal
        {
            get
            {
                MenuItem item = new MenuItem();
                decimal subtotal = item.Price * Quantity;
                return subtotal;
            }
        }


        [Display(Name = "Tip Amount")]
        public decimal Gratuity
        {
            get
            {
                decimal tip = 0;
                return tip;
            }
            set
            {

            }
        }

        [Display(Name = "Grand Total")]
        public decimal GrandTotal
        {
            get
            {
                decimal grandTotal = (SetTax * Subtotal) + Gratuity;
                return grandTotal;
            }
        }

    }
}
