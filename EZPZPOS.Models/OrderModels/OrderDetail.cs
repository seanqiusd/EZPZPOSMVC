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

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Guest Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        // Testing the follwoing 
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }

        [Display(Name = "Order Date/Time")]
        public DateTimeOffset OrderDateTimeUtc { get; set; }

        [Display(Name = "Order Type")]
        public OrderType TypeOfOrder { get; set; }

        [Display(Name = "Menu Item ID")]
        public int MenuItemId { get; set; }

        // Trying this just in the model and controller
        public double ServingsInStock { get; set; }
        public bool IsAvailable { get; set; }

        [Display(Name = "Menu Item Name")]
        public string Name { get; set; }

        [Display(Name = "Quantity Needed")]
        public int Quantity { get; set; }

        [Display(Name = "Special Instructions")]
        public string Notes { get; set; }


        [Display(Name = "Tax Rate")]
        public decimal SetTax { get; set; }

        public decimal Subtotal { get; set; }


        [Display(Name = "Tip Amount")]
        public decimal Gratuity { get; set; }

        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

    }
}
