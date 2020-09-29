using EZPZPOS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Models.GuestModels
{
    public class GuestDetail
    {
        [Display(Name = "Guest ID")]
        public int GuestId { get; set; }

        [Display(Name = "Guest First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Guest Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }

        public string Notes { get; set; }

        [Display(Name = "First Visit")] // likely going to remove this from here and from data bc this is a stretch goal that should be in services
        public bool FirstTime
        {
            get
            {
                Order order = new Order();
                if (GuestId == order.GuestId)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [Display(Name = "Date Of Last Visit")] // likely going to remove this from here and from data bc this is a stretch goal that should be in services
        public DateTimeOffset LastVisitUtc { get; set; }



    }
}
