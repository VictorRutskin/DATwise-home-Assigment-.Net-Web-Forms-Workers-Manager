using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JScriptHandler
    {
        private readonly static string _activate_ShowPopup = @" $(document).ready(function() {window.showPopup();} );";
        
        public static string Redirect_EmployeesList(int milliseconds)
        {
            return $"setTimeout(function() {{ window.location = 'EmployeesList'; }}, {milliseconds});\r\n";
        }
        public static string Activate_ShowPopup()
        {
            return _activate_ShowPopup;
        }

    }
}
