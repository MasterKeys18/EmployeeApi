﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using EmployeesApi.Models;

namespace EmployeesApi.App_Data
{
    public static class Repository
    {
        private const string ConnectionString = @"Data Source=LAPTOP-JOE7E5FJ\SQLEXPRESS;Initial Catalog=employees;Integrated Security=True";

        public static List<EmployeeClientModel> GetEmployees()
        {
            List<EmployeeDbModel> employees;
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                employees = db.Query<EmployeeDbModel>
                (@" select
                e.id,
                e.name,
                e.surname,
                e.phone,
                e.companyId,
                p.type,
                p.number
                    from Employee as e
                join Passport as p on
                e.passport = p.id").ToList();
                
            }
            return employees.Select(e => new EmployeeClientModel
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                Name = e.Name,
                Surname = e.Surname,
                Phone = e.Phone,
                Passport = new Passport
                {
                    Number = e.Number,
                    Type = e.Type
                }
            }).ToList();
        }
 
        public static EmployeeClientModel Get(int id)
        {
            EmployeeDbModel employee;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                employee = db.Query<EmployeeDbModel>
                (@"select
                e.id,
                e.name,
                e.surname,
                e.phone,
                e.companyId,
                p.type,
                p.number
                    from Employee as e
                join Passport as p on
                e.passport = p.id WHERE e.id = @id", new { id }).FirstOrDefault();
            }

            if (employee == null)
                return null;
            return new EmployeeClientModel
            {
                Id = employee.Id,
                CompanyId = employee.CompanyId,
                Name = employee.Name,
                Surname = employee.Surname,
                Phone = employee.Phone,
                Passport = new Passport
                {
                    Number = employee.Number,
                    Type = employee.Type
                }
            };
        }
        private static EmployeeDbModel GetEmployee(int id)
        {
            EmployeeDbModel employee;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                employee = db.Query<EmployeeDbModel>
                (@"select
                e.id,
                e.name,
                e.surname,
                e.phone,
                e.companyId,
                p.type,
                p.number
                    from Employee as e
                join Passport as p on
                e.passport = p.id WHERE e.id = @id", new { id }).FirstOrDefault();
            }

            return employee;
        }
        public static int Create(EmployeeClientModel employeeModel)
        {
            var employee = new EmployeeDbModel
            {
                Id = employeeModel.Id,
                Name = employeeModel.Name,
                Surname = employeeModel.Surname,
                Phone = employeeModel.Phone,
                CompanyId = employeeModel.CompanyId,
                Number = employeeModel.Passport.Number,
                Type = employeeModel.Passport.Type
            };
            int employeeId;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                const string sqlQueryPassport = "INSERT INTO Passport (type, number) VALUES (@type, @number); SELECT CAST(SCOPE_IDENTITY() as int)";
                var passportId = db.Query<int>(sqlQueryPassport, new {type = employee.Type, number = employee.Number}).FirstOrDefault();
                const string sqlQueryEmployee = "INSERT INTO Employee (name, surname, phone, companyId, passport) VALUES(@name, @surname, @phone, @companyId, @passportId); SELECT CAST(SCOPE_IDENTITY() as int)";
                employeeId = db.Query<int>(sqlQueryEmployee, employee).FirstOrDefault();
            }

            return employeeId;
        }
 
        public static void Update(EmployeeClientModel employeeModel)
        {
            var employee = GetEmployee(employeeModel.Id);
            employee.Name = employeeModel.Name ?? employee.Name;
            employee.Surname = employeeModel.Surname ?? employee.Surname;
            employee.Phone = employeeModel.Phone ?? employee.Phone;
            employee.CompanyId = employeeModel.CompanyId ?? employee.CompanyId;
            employee.Type = employeeModel.Passport?.Type ?? employee.Type;
            employee.Number = employeeModel.Passport?.Number ?? employee.Number;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                const string sqlQueryPassport = "update Passport set type = @type, number = @number where id = @id";
                db.Execute(sqlQueryPassport, employee);
                
                const string sqlQueryEmployee = "UPDATE Employee SET name = @name, surname = @surname, phone = @phone, companyId = @companyId  WHERE id = @id";
                db.Execute(sqlQueryEmployee, employee);
            }
        }

        public static void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                const string sqlQuery = "DELETE FROM Employee WHERE id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}