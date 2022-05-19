using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class BusinessAmenities:BaseEntity
    {
        [Required]
        public virtual int BusinessId { get; set; }
        public Business Business { get; set; }
        [Required]
        public virtual int AmenityId { get; set; }
        public virtual Amenity Amenity { get; set; }
    }
}
