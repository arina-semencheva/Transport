using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Models;

namespace Transport.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {

        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Roles
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            var roles = context.Roles.ToList();
            return View(roles);

        }

        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                return s[0].ToString() == "Admin";
            }
            return false;
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}