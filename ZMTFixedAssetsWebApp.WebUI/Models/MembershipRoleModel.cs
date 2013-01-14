using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using ZMTFixedAssetsWebApp.WebUI.Validation;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class MembershipRoleModel
    {
        [RoleNameValidation(ErrorMessage = "Podana rola instnieje. Podaj inną nazwę")]
        [StringLength(20, ErrorMessage = "Nazwa roli powinno zawierać maksymalnie 30 znaków")]
        [Required(ErrorMessage = "Pole nazwa jest wymanage")]
        public string Name { get; set; }

        public string [] Users { get; set; }
        
        public SelectList SubscriptionSources { get; set; } // This property contains the available options
        public IEnumerable<string> SelectedSources { get; set; } // This property contains the selected options

        public MembershipRoleModel()
        {
            Users = new string[] { };
            SelectedSources = Users;
        }

        public MembershipRoleModel(string Name, string[] Users)
        {
            this.Name = Name;
            this.Users = Users;
            LoadCheckBoxList();
        }

        public void LoadCheckBoxList()
        {
            SubscriptionSources = new SelectList(Membership.GetAllUsers());
            SelectedSources = Users;
        }

    }
}