using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Area> Areas { get; set; }
     
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
