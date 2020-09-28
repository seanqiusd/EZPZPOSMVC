using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Guest))]
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }

        [Required]
        [Display(Name ="Order Date/Time")]
        public DateTimeOffset OrderDateTimeUtc { get; set; }

        [Required]
        [Display(Name ="Order Type")]
        public OrderType TypeOfOrder { get; set; }

        [ForeignKey(nameof(MenuItem))]
        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        [Required]
        [Display(Name ="Quantity Needed")]
        [Range(1, 10000, ErrorMessage ="Quantity must be at least 1")]
        public int Quantity { get; set; }

        public string Notes { get; set; }

       
        [Display(Name ="Tax Rate")]
        public decimal SetTax
        {
            get
            {
                decimal taxRate = 0.09m;
                return taxRate;
            }
        }

        public decimal Subtotal
        {
            get
            {
                MenuItem item = new MenuItem();
                decimal subtotal = item.Price* Quantity;
                return subtotal;
            }
        }


        [Display(Name ="Tip Amount")]
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

        [Display(Name ="Grand Total")]
        public decimal GrandTotal
        {
            get
            {
                decimal grandTotal = (SetTax * Subtotal) + Gratuity;
                return grandTotal;
            }
        }


    }

    public enum OrderType { PickUp, Delivery, DineIn}
}
