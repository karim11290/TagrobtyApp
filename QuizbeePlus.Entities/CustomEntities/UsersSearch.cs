using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities.CustomEntities
{
    public class UsersSearch
    {
        public List<UserWithRoleEntity> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
