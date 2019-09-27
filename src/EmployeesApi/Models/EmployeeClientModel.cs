﻿namespace EmployeesApi.Models
{
    public class EmployeeClientModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public int? companyId { get; set; }
        public Passport passport { get; set; }
    }

    public class Passport
    {
        public string type { get; set; }
        public string number { get; set; }
    }
}