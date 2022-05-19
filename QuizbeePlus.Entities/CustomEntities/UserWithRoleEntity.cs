using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities.CustomEntities
{
    public class UserWithRoleEntity
    {
        public QuizbeeUser User { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
