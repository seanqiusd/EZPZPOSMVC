using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Data
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [ForeignKey(nameof(User))]
        public string ServerId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string ContactNumber { get; set; }

        [Display(Name ="Full Address")]
        public string FullAddress { get; set; }

        public string Notes { get; set; }


        [Display(Name ="First Visit: True/False")]
        public bool FirstTime { get; set; }

    }
}
