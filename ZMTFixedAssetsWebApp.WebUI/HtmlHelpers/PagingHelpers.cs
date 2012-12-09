using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Mvc.Ajax;

namespace ZMTFixedAssetsWebApp.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {

        public static MvcHtmlString Pager(this AjaxHelper helper,int CurrentPage, int TotalRecords, string TargetDiv, Dictionary<string, object> dict, string ControllerName, string ActionName, int PageSize)
        {
             if (TotalRecords > 0)
             {
                StringBuilder sb = new StringBuilder();
                int TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);
  
                //Build the Ajax Options
                AjaxOptions ao = new AjaxOptions();
                ao.UpdateTargetId = TargetDiv;
                ao.HttpMethod = "GET";
                ao.OnFailure = "OnFailure";
                dict.Add("page", 1);

                if (CurrentPage - 1 > 0)
                {
                   dict["page"] = 1;
                   sb.Append(System.Web.Mvc.Ajax.AjaxExtensions.ActionLink(helper, "<<", ActionName, new System.Web.Routing.RouteValueDictionary(dict), ao));
                   sb.Append("  ");

                   dict["page"] = CurrentPage - 1;
                   sb.Append(System.Web.Mvc.Ajax.AjaxExtensions.ActionLink(helper, "<", ActionName, new System.Web.Routing.RouteValueDictionary(dict), ao));
                   sb.Append("  ");

                }
  
                //Add the Page Number
                sb.Append("Page " + CurrentPage.ToString() + " of " + (TotalPages).ToString());
  
                if (CurrentPage < (TotalPages))
                {
                   //Add the Next Links
                   dict["page"] = CurrentPage + 1;
                   sb.Append("  ");
                   sb.Append(System.Web.Mvc.Ajax.AjaxExtensions.ActionLink(helper, ">", ActionName, new System.Web.Routing.RouteValueDictionary(dict), ao));
                   dict["page"] = TotalPages;
                   sb.Append("  ");
                   sb.Append(System.Web.Mvc.Ajax.AjaxExtensions.ActionLink(helper, ">>", ActionName, new System.Web.Routing.RouteValueDictionary(dict), ao));
                }
                
                return MvcHtmlString.Create(sb.ToString());
             }
             else
             {
                //Don't return anything for the pager if we do not have any records
                return MvcHtmlString.Create("");
             }
        } 
    }
}
