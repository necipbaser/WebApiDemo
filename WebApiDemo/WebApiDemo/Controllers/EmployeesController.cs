using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Web.Http.Description;

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


        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities empEntities = new EmployeeDBEntities())
            {
                var entity = empEntities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    var error = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID =" + id.ToString() + " not found.");
                    return error;
                }

            }
        }

        public HttpResponseMessage Post([FromBody] Employees employee)
        {
            try
            {
                using (EmployeeDBEntities empEntities = new EmployeeDBEntities())
                {
                    empEntities.Employees.Add(employee);
                    empEntities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID=" + id.ToString() + " not found for delete.");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, Employees employee)
        {
            try
            { 
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Employee with ID=" + id.ToString() + " not found to update.");
                }
                else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
    }
}
