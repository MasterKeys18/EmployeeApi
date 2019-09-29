using System.Collections.Generic;
using System.Web.Http;
using EmployeesApi.App_Data;
using EmployeesApi.Models;

namespace EmployeesApi.Controllers
{
    [RoutePrefix("api/employee")]
    /**
     * Класс-контроллер, регулирующий взаимодействие программы с базой данный сотрудников
     *
     * @author Gorbacheva Olga
     */
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("all")]
        /**
         * Возвращает список моделей данный о всех пользователях
         */
        public List<EmployeeClientModel> GetAllEmployees()
        {
            return Repository.GetEmployees();
        }
        
        [HttpGet]
        [Route("{id}")]
        /**
         * Возвращает модель данных пользователя согласно указанному id
         */
        public EmployeeClientModel GetById(int id)
        {
            return Repository.Get(id);
        }

        [HttpPost]
        [Route("create")]
        /**
         * Возвращает id добавленного сотрудника
         */
        public int Create(EmployeeClientModel employee)
        {
            return Repository.Create(employee);
        }
 
        [HttpPut]
        [Route("update")]
        /**
         * Метод, позволяющий обновить данные сотрудника
         */
        public void Update(EmployeeClientModel employee)
        {
            Repository.Update(employee);
        }
        
        [HttpDelete]
        [Route("{id}")]
        /**
         * Метод, позволяющий удалить данные сотрудника согласно указанному id
         */
        public void Delete(int id)
        {
            Repository.Delete(id);
        }
    }
}