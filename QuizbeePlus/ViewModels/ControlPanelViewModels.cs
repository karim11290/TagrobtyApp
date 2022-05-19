using Microsoft.AspNet.Identity.EntityFramework;
using QuizbeePlus.Entities;
using QuizbeePlus.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizbeePlus.ViewModels
{
    public class ControlPanelViewModel : BaseViewModel
    {
        public bool isPartial { get; set; }
    }

    public class UsersListingViewModel : ListingBaseViewModel
    {
        public List<UserWithRoleEntity> Users { get; set; }
    }

    public class UserDetailsViewModel : BaseViewModel
    {
        public UserWithRoleEntity User { get; set; }
        public List<IdentityRole> AvailableRoles { get; set; }
    }

    public class RolesListingViewModel : ListingBaseViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }

    public class NewRoleViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class UpdateRoleViewModel : BaseViewModel
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class RoleDetailsViewModel : BaseViewModel
    {
        public RoleWithUsersEntity Role { get; set; }
    }

 
}