using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Web_Forms
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            string baseUrl = "~/Pages";

            // Default 
            routes.MapPageRoute(
                "DefaultRoute", // routeName
                "", // routeUrl
                $"{baseUrl}/Default.aspx" // file location
            );
            // About 
            routes.MapPageRoute(
                "AboutRoute",
                "About",
                $"{baseUrl}/About.aspx"
            );
            // Contact 
            routes.MapPageRoute(
                "ContactRoute",
                "Contact",
                 $"{baseUrl}/Contact.aspx"
            );
            // EmployeeForm 
            routes.MapPageRoute(
                "EmployeesFormRoute",
                "EmployeesForm",
                 $"{baseUrl}/EmployeesForm.aspx"
            );
            // EmployeeList 
            routes.MapPageRoute(
                "EmployeesListRoute",
                "EmployeesList",
                 $"{baseUrl}/EmployeesList.aspx"
            );

            //  Invalid URLs (catches all the leftovers)
            routes.MapPageRoute(
                "InvalidRoutes",
                "{*url}",
                "~/Pages/Default.aspx"
            );

            routes.EnableFriendlyUrls(settings);
        }
    }
}
