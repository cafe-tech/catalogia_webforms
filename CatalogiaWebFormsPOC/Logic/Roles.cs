using System;
using System.Collections.Generic;
using System.Linq;

using CatalogiaWebForms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CatalogiaWebForms.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            // DEFAULT: Access the application context and create result variables. 
            Models.ApplicationDbContext context = new ApplicationDbContext();

            // If you want the membership / users saved along with your application data, then connect to the 
            // DbContext created for the application data instead
            // Models.CatalogObjectContext context = new CatalogObjectContext();

            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists("canEdit"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "canEdit" });
            }

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "canEditUser@catalogia.com",
                Email = "canEditUser@catalogia.com"
            };
            IdUserResult = userMgr.Create(appUser, "Password1!");

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            /*
            if (!userMgr.IsInRole(userMgr.FindByEmail("canEditUser@catalogia.com").Id, "canEdit"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("canEditUser@catalogia.com").Id, "canEdit");
            }
            */

            if (!userMgr.IsInRole(userMgr.FindByEmail("youremail@yourdomain.com").Id, "canEdit"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("youremail@yourdomain.com").Id, "canEdit");
            }
        }
    }
}