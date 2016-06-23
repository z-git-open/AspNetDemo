using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeDemo.Security
{
    public class IdentityService
    {
        public string GetCurrentUser()
        {
            return System.Threading.Thread.CurrentPrincipal.Identity.Name;
        }
    }
}