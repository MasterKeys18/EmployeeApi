﻿namespace EmployeesApi.Models
{
/**
 * Класс, отображающий модель данных сотрудника согласно представлению в базе данных
 *
 * @author Gorbacheva Olga
 */
 public class EmployeeClientModel
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
         * Паспорт сотрудника
         */
        public Passport Passport { get; set; }
    }

    public class Passport
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }
}