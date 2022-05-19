using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Area:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<QuizbeeUser> Users  { get; set; }
        public virtual List<Business>Businesses { get; set; }
        [Required]
        public virtual int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
