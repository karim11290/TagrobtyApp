using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int DisplaySeqNo { get; set; }
        public virtual List<Business> Businesses { get; set; }
        public string Photo { get; set; }
    }
}
