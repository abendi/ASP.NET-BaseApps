using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image : BaseEntity
    {
        public int ImageId { get; set; }

        public string ImageUrl { get; set; }

    }
}
