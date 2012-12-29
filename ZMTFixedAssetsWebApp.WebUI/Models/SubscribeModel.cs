using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.Models
{
    public class SubscribeModel
    {

        // This property contains the available options
        public SelectList SubscriptionSources { get; set; }

        // This property contains the selected options
        public IEnumerable<string> SelectedSources { get; set; }

        public SubscribeModel()
        {
            SubscriptionSources = new SelectList(
                new[] { "Google", "TV", "Radio", "A friend", "Crazy bloke down the pub" });

            SelectedSources = new[] { "Google" };
        }
    }
}