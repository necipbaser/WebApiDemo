using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        
        public IEnumerable<Employees> Get()
        {
            using (EmployeeDBEntities empEntities = new EmployeeDBEntities())
            {                
                return empEntities.Employees.ToList();
            }
        }

        public Employees Get(int id)
        {
            using (EmployeeDBEntities empEntities = new EmployeeDBEntities())
            {
                return empEntities.Employees.FirstOrDefault(e => e.ID == id);
            }
        }
    }
}
