using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Business:BaseEntity
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public virtual List<Image> Images { get; set; }

        public virtual List<OpeningHours> OpeningHoursList { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
