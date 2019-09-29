﻿namespace EmployeesApi.Models
{
 /**
  * Класс, отображающий все данные о сотруднике в одной модели
  *
  * Данный класс является вспомогательным и необходим для корректной работы с данными,
  * так как присутствует вложенная модель данных (паспорт)
  *
  * @author Gorbacheva Olga
  */
 public class EmployeeDbModel
    {
        /**
         * Id сотрудника
         */
        public int Id { get; set; }
        /**
         * Имя сотрудника
         */
        public string Name { get; set; }
        /**
         * Фамилия сотрудника
         */
        public string Surname { get; set; }
        /**
         * Телефон сотрудника
         */
        public string Phone { get; set; }
        /**
         * Id компании сотрудника
         */
        public int? CompanyId { get; set; }
        /**
         * Серия паспорта сотрудника
         */
        public string Type { get; set; }
        /**
         * Номер паспорта сотрудника
         */
        public string Number { get; set; }
    }
}