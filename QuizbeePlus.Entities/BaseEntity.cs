using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; private set; }

        //public bool IsActive { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
    }
}
