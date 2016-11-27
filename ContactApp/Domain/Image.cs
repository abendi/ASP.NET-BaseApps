using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image : BaseEntity
    {
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Filename is required")]
        public string ImageUrl { get; set; }

    }
}
