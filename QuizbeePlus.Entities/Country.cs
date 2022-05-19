using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Country:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<City>Cities { get; set; }
    }
}
