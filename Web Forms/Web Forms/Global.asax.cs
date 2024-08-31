using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Web_Forms
{
    public class Global : HttpApplication
    {
        private readonly string baseUrl = "~/Pages";

        void Application_Start(object sender, EventArgs e)
        {


            // Default 
            RouteTable.Routes.MapPageRoute("DefaultRoute", "", $"{baseUrl}/Default.aspx");
            // About 
            RouteTable.Routes.MapPageRoute("AboutRoute", "About", $"{baseUrl}/About.aspx");
            // Contact 
            RouteTable.Routes.MapPageRoute("ContactRoute", "Contact", $"{baseUrl}/Contact.aspx");
            // EmployeesForm 
            RouteTable.Routes.MapPageRoute("EmployeesFormRoute", "EmployeesForm", $"{baseUrl}/EmployeesForm.aspx");
            // EmployeesList 
            RouteTable.Routes.MapPageRoute("EmployeesListRoute", "EmployeesList", $"{baseUrl}/EmployeesList.aspx");


            // Invalid URLs (catches all the leftovers)
            RouteTable.Routes.MapPageRoute("InvalidRoutes", "{*url}", $"{baseUrl}/Default.aspx");
        }

    }
}