using System;

namespace Common
{
    public static class JScriptHandler
    {
        private readonly static string _activate_ShowPopup = @"$(document).ready(function() { window.showPopup(); });";

        public static string Redirect_EmployeesList(int milliseconds)
        {
            return $"setTimeout(function() {{ window.location.href = 'EmployeesList'; }}, {milliseconds});\r\n";
        }

        public static string Redirect_UpdateForm(int id)
        {
            return $"setTimeout(function() {{ window.location.href = 'EmployeesForm?EmployeeID={id}'; }}, 200);\r\n";
        }

        public static string Activate_ShowPopup()
        {
            return _activate_ShowPopup;
        }
    }
}
