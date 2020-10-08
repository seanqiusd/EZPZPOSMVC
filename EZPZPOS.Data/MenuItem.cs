using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Data
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage ="At least 1 character required")]
        [Display(Name ="Menu Item Name")]
        public string Name { get; set; }

        [MinLength(1, ErrorMessage ="At least 1 character required")]
        public string Description { get; set; }

        [Required]
        [MinLength(1, ErrorMessage ="At least 1 character required")]
        public string Category { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage ="Price must be between 0.01 and 10000.00")]
        public decimal Price { get; set; }

        [Display(Name ="Servings In Stock")]
        [Range(0, int.MaxValue, ErrorMessage ="Can't have less than 0")]
        public double ServingsInStock { get; set; }

        [Display(Name ="Is Available: True/False")]
        public bool IsAvailable { get; set; }
        
    }
}
