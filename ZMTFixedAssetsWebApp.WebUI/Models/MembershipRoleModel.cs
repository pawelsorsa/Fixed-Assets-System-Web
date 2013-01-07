using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class MembershipRoleModel
    {
        public string Name { get; set; }
        public string [] Users { get; set; }
        public SelectList SubscriptionSources { get; set; } // This property contains the available options
        public IEnumerable<string> SelectedSources { get; set; } // This property contains the selected options

        public MembershipRoleModel()
        {
            SubscriptionSources = null;
            SelectedSources = new string[] { };
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