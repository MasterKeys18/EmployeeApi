using System.Collections.Generic;
using System.Web.Http;
using EmployeesApi.DataAccess;
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
            return EmployeeRepository.GetEmployees();
        }
        
        [HttpGet]
        [Route("{id}")]
        /**
         * Возвращает модель данных пользователя согласно указанному id
         */
        public EmployeeClientModel GetById(int id)
        {
            return EmployeeRepository.Get(id);
        }

        [HttpPost]
        [Route("create")]
        /**
         * Возвращает id добавленного сотрудника
         */
        public int Create(EmployeeClientModel employee)
        {
            return EmployeeRepository.Create(employee);
        }
 
        [HttpPut]
        [Route("update")]
        /**
         * Метод, позволяющий обновить данные сотрудника
         */
        public void Update(EmployeeClientModel employee)
        {
            EmployeeRepository.Update(employee);
        }
        
        [HttpDelete]
        [Route("{id}")]
        /**
         * Метод, позволяющий удалить данные сотрудника согласно указанному id
         */
        public void Delete(int id)
        {
            EmployeeRepository.Delete(id);
        }
    }
}