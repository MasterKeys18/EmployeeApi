using System.Collections.Generic;
using System.Web.Http;
using EmployeesApi.App_Data;
using EmployeesApi.Models;

namespace EmployeesApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public List<EmployeeClientModel> GetAllEmployees()
        {
            return Repository.GetEmployees();
        }
        
        [HttpGet]
        [Route("{id}")]
        public EmployeeClientModel GetById(int id)
        {
            return Repository.Get(id);
        }

        [HttpPost]
        [Route("create")]
        public int Create(EmployeeClientModel employee)
        {
            return Repository.Create(employee);
        }
 
        [HttpPut]
        [Route("update")]
        public void Edit(EmployeeClientModel employee)
        {
            Repository.Update(employee);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Repository.Delete(id);
        }
    }
}