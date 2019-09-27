﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using EmployeesApi.Models;

namespace EmployeesApi.App_Data
{
    public class Repository
    {
        private const string ConnectionString =
            @"Data Source=LAPTOP-JOE7E5FJ\SQLEXPRESS;Initial Catalog=employees;Integrated Security=True";
        

        public List<EmployeeClientModel> GetUsers()
        {
            List<EmployeeDbModel> employees;
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                employees = db.Query<EmployeeDbModel>(@"	select
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
            return employees?.Select(e => new EmployeeClientModel
            {
                id = e.id,
                companyId = e.companyId,
                name = e.name,
                surname = e.surname,
                phone = e.phone,
                passport = new Passport
                {
                    number = e.number,
                    type = e.type
                }
            }).ToList();
        }
 
        public EmployeeClientModel Get(int id)
        {
            EmployeeDbModel employee;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                employee = db.Query<EmployeeDbModel>(@"	select
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
                id = employee.id,
                companyId = employee.companyId,
                name = employee.name,
                surname = employee.surname,
                phone = employee.phone,
                passport = new Passport
                {
                    number = employee.number,
                    type = employee.type
                }
            };
        }
        private EmployeeDbModel GetEmployee(int id)
        {
            EmployeeDbModel employee;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                employee = db.Query<EmployeeDbModel>(@"	select
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
        public int Create(EmployeeClientModel employeeModel)
        {
            var employee = new EmployeeDbModel
            {
                id = employeeModel.id,
                name = employeeModel.name,
                surname = employeeModel.surname,
                phone = employeeModel.phone,
                companyId = employeeModel.companyId,
                number = employeeModel.passport.number,
                type = employeeModel.passport.type
            };
            int employeeId;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery1 =
                    "INSERT INTO Passport (type, number) VALUES (@type, @number); SELECT CAST(SCOPE_IDENTITY() as int)";
                var passportId = db.Query<int>(sqlQuery1, new {type = employee.type, number = employee.number}).FirstOrDefault();
                employee.passportId = passportId;
                var sqlQuery2 = "INSERT INTO Employee (name, surname, phone, companyId, passport) VALUES(@name, @surname, @phone, @companyId, @passportId); SELECT CAST(SCOPE_IDENTITY() as int)";
                employeeId = db.Query<int>(sqlQuery2, employee).FirstOrDefault();
            }

            return employeeId;
        }
 
        public void Update(EmployeeClientModel employeeModel)
        {
            var emp = GetEmployee(employeeModel.id);
            emp.name = employeeModel.name ?? emp.name;
            emp.surname = employeeModel.surname ?? emp.surname;
            emp.phone = employeeModel.phone ?? emp.phone;
            emp.companyId = employeeModel.companyId ?? emp.companyId;
            emp.type = employeeModel.passport?.type ?? emp.type;
            emp.number = employeeModel.passport?.number ?? emp.number;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery1 = "update Passport set type = @type, number = @number where id = @id";
                db.Execute(sqlQuery1, emp);
                
                var sqlQuery = "UPDATE Employee SET name = @name, surname = @surname, phone = @phone, companyId = @companyId  WHERE id = @id";
                db.Execute(sqlQuery, emp);
            }
        }
 
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "DELETE FROM Employee WHERE id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}