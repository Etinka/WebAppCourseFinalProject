using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCourseFinalProject.Models;

namespace WebAppCourseFinalProject.Utils
{
    public static class Utilities
    {
        public static string IsActive(this IHtmlHelper html,
                                      string control,
                                      string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control == routeControl &&
                               action == routeAction;

            return returnActive ? "active" : "";
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
                  this IEnumerable<Post> posts, int selectedId)
        {
            return
                posts.OrderBy(post => post.Writer.DisplayName)
                      .Select(post =>
                          new SelectListItem
                          {
                              Selected = (post.Writer.Id == selectedId),
                              Text = post.Writer.DisplayName,
                              Value = post.Writer.Id.ToString()
                          });
        }
    }
}
