using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZMTFixedAssetsWebApp.WebUI.HtmlHelpers;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class MembershipUserModel
    {
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastLockoutDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public string PasswordQuestion { get; set; }
        public string ProviderName { get; set; }
        public object ProviderUserKey { get; set; }
        public string UserName { get; set; }
        public SelectList SubscriptionSources { get; set; } // This property contains the available options
        public IEnumerable<string> SelectedSources { get; set; } // This property contains the selected options
      
        public MembershipUserModel()
        {
            SubscriptionSources = null;
            SelectedSources = new [] { "" };

        }


        public MembershipUserModel(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate, bool isOnline)
        {
            this.ProviderName = providerName;
            this.UserName = name;
            this.ProviderUserKey = providerUserKey;
            this.Email = email;
            this.PasswordQuestion = passwordQuestion;
            this.Comment = comment;
            this.IsOnline = isOnline;
            this.IsApproved = isApproved;
            this.IsLockedOut = isLockedOut;
            this.CreationDate = creationDate; 
            this.LastLoginDate = lastLoginDate; 
            this.LastActivityDate = lastActivityDate; 
            this.LastPasswordChangedDate = lastPasswordChangedDate;
            this.LastLockoutDate = lastLockoutDate;
            LoadCheckBoxList();      
        }


        private void LoadCheckBoxList()
        {
            SubscriptionSources = new SelectList(Roles.GetAllRoles());
            SelectedSources = Roles.GetRolesForUser(UserName);
        }

        
       
    }
}