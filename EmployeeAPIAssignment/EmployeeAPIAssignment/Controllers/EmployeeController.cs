using EmployeeAPIAssignment.DB_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace EmployeeAPIAssignment.Controllers
{
    [ApiController]
    public class EmployeeController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        readonly DB DbLayer = new DB();

        // GET: Employee
        [System.Web.Http.HttpGet]
        public ActionResult<DataSet> GetAllEmployees()
        {
            DataSet ds = DbLayer.GetAllEmployees();
            return ds;
        }
    }
}