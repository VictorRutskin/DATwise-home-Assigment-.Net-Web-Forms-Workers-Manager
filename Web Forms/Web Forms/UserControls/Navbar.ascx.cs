using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Forms.UserControls
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string IsActivePage(string page)
        {
            var currentUrl = HttpContext.Current.Request.Url.AbsolutePath;

            // Check if the current URL contains the menu page
            return currentUrl.Contains(page) ? "active" : string.Empty;
        }

    }
}