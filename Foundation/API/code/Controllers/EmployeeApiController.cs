using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EmployeeDataAccess;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Sitecore.Foundation.API.Controllers
{
    public class EmployeeApiController : Controller
    {
        public ActionResult Get()
        {
            using (EmployeesDBEntities entities = new EmployeesDBEntities())
            {
                
                return Json(entities.Employees.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
