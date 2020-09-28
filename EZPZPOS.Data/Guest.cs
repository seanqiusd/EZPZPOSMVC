using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Data
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [ForeignKey(nameof(UserId))]
        public string ServerId { get; set; }
        public virtual ApplicationUser UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string ContactNumber { get; set; }

        [Display(Name ="Full Address")]
        public string FullAddress { get; set; }

        public string Notes { get; set; }

        [Display(Name ="First Visit: True/False")]
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

        [Display(Name ="Date Of Last Visit")]
        public DateTimeOffset LastVisitUtc { get; set; }


    }
}
