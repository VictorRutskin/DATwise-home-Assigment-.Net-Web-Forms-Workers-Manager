using System;

namespace Common
{
    // Small Js scripts for injection to ScriptManager
    public static class JScriptHandler
    {
        public static string Redirect_EmployeesList(int milliseconds)
        {
            return $"setTimeout(function() {{ window.location.href = 'EmployeesList'; }}, {milliseconds});\r\n";
        }

        // Unused at the moment
        public static string Redirect_UpdateForm(int id)
        {
            return $"setTimeout(function() {{ window.location.href = 'EmployeesForm?EmployeeID={id}'; }}, 200);\r\n";
        }

        public static string Activate_ShowPopup()
        {
            return @"$(document).ready(function() { window.showPopup(); });";
        }
    }
}
