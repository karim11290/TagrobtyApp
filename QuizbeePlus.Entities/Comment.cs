using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Comment:BaseEntity
    {
        [Required]
        public  int BusinessId { get; set; }
        public virtual Business Business { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual QuizbeeUser QuizbeeUser { get; set; }


        public int Rate { get; set; }
        public string Text { get; set; }
    }
}
