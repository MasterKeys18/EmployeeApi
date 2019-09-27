﻿namespace EmployeesApi.Models
{
    public class EmployeeDbModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public int? companyId { get; set; }
        public int passportId { get; set; }
        public string type { get; set; }
        public string number { get; set; }
    }
}