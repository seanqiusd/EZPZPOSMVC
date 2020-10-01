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

    }
}
