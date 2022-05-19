using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class OpeningHours:BaseEntity
    {
        [Required]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        [Required]
        public int BusinessId { get; set; }
        public virtual Business Business { get; set; }
        public int From  { get; set; }
        public int To { get; set; }
    }
}
