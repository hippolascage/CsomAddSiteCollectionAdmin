using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace SpoAddSiteCollectionAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];
            var siteUrl = ConfigurationManager.AppSettings["siteCollecitonUrl"];
            var newAdminLogonName = ConfigurationManager.AppSettings["newAdminLogonName"];

            var securePassword = new SecureString();
            foreach (var c in password.ToCharArray()) securePassword.AppendChar(c);

            var clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials(username, securePassword);

            var newAdmin = clientContext.Web.EnsureUser(newAdminLogonName);
            newAdmin.IsSiteAdmin = true;
            newAdmin.Update();
            clientContext.Load(newAdmin);
            clientContext.ExecuteQuery();
        }
    }
}
