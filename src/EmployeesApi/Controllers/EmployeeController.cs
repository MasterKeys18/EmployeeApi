using System.Collections.Generic;
using System.Web.Http;
using EmployeesApi.App_Data;
using EmployeesApi.Models;

namespace EmployeesApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        Repository repo = new Repository();
        
        [HttpGet]
        [Route("all")]
        public List<EmployeeClientModel> Get()
        {
            return repo.GetUsers();
        }
        
        
        [HttpGet]
        [Route("{id}")]
        public EmployeeClientModel Details(int id)
        {
            var user = repo.Get(id);
            return user;
        }

        [HttpPost]
        [Route("create")]
        public int Create(EmployeeClientModel user)
        {
            return repo.Create(user);
        }
 
        [HttpPut]
        [Route("update")]
        public void Edit(EmployeeClientModel user)
        {
            repo.Update(user);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}