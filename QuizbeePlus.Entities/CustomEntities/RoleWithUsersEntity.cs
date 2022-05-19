using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities.CustomEntities
{
    public class RoleWithUsersEntity
    {
        public IdentityRole Role { get; set; }
        public List<QuizbeeUser> Users { get; set; }
    }
}
