using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Amenity:BaseEntity
    {
        public string Name { get; set; }
        public virtual List <BusinessAmenities> BusinessAmenitiesList { get; set; }
        public string Photo { get; set; }
    }
}
