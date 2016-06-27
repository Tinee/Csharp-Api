using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;

namespace Mappers
{
    public static class Mappers
    {
        #region ToContracts

        public static List<Office> ToContracts(this IEnumerable<R1> dataBaseOffices)
        {
            return dataBaseOffices.ToList().ConvertAll(x => new Office
            {
                Id = x.RNo,
                Name = x.Nm
            });
        }

        public static List<Project> ToContracts(this IEnumerable<R9> dataBaseProjects)
        {
            return dataBaseProjects.ToList().ConvertAll(x => new Project
            {
                Name = x.Nm,
                Id = x.RNo
            });
        }

        public static List<Activity> ToContracts(this IEnumerable<Prod> dataBaseActivities)
        {
            return dataBaseActivities.ToList().ConvertAll(x => new Activity
            {
                Id = x.ProdNo,
                Description = x.Descr,
            });
        }

        public static List<Agreement> ToContracts(this IEnumerable<R5> dataBaseAgreements)
        {
            return dataBaseAgreements.ToList().ConvertAll(x => new Agreement
            {
                Id = x.RNo,
                Name = x.Nm
            });
        }

        public static List<Service> ToContracts(this IEnumerable<R8> dataBaseServices)
        {
            return dataBaseServices.ToList().ConvertAll(x => new Service
            {
                Id = x.RNo,
                Description = x.Nm
            });
        }

        public static List<WorkText> ToContracts(this IEnumerable<Txt> dataBaseWorkTexts)
        {
            return dataBaseWorkTexts.ToList().ConvertAll(x => new WorkText
            {
                Id = x.TxtNo,
                Text = x.Txt1,
                GroupId = x.TxtTp
            });
        }

        public static List<Customer> ToCustomers(this IEnumerable<Actor> dataBaseCustomer)
        {
            return dataBaseCustomer.ToList().ConvertAll(x => new Customer
            {
                CustomerId = x.CustNo,
                Adress = x.Ad1,
                City = x.PArea,
                Email = x.MailAd,
                Name = x.Nm,
                PhoneNumber = x.Phone,
                PostalCode = x.PNo,
            });
        }

        public static List<Employee> ToEmployees(this IEnumerable<Actor> dataBaseEmployees)
        {
            return dataBaseEmployees.ToList().ConvertAll(x => new Employee
            {
                EmployeeId = x.EmpNo,
                Adress = x.Ad1,
                City = x.PArea,
                Email = x.MailAd,
                Name = x.Nm,
                PhoneNumber = x.Phone,
                PostalCode = x.PNo,
                Username = x.Usr,
                OfficeId = x.R1
            });
        }

        #endregion

        #region EntitieConverter

        public static Office ToContract(this R1 dataBaseOffice)
        {
            return new Office
            {
                Id = dataBaseOffice.RNo,
                Name = dataBaseOffice.Nm
            };
        }

        public static Agreement ToContract(this R5 dataBaseAgreement)
        {
            if (dataBaseAgreement == null) return null;
            return new Agreement
            {
                Id = dataBaseAgreement.RNo,
                Name = dataBaseAgreement.Nm
            };
        }


        public static Employee ToEmployee(this Actor dataBaseEmployee)
        {
            return new Employee
            {
                EmployeeId = dataBaseEmployee.EmpNo,
                Adress = dataBaseEmployee.Ad1,
                City = dataBaseEmployee.PArea,
                Email = dataBaseEmployee.MailAd,
                Name = dataBaseEmployee.Nm,
                PhoneNumber = dataBaseEmployee.Phone,
                PostalCode = dataBaseEmployee.PNo,
                Username = dataBaseEmployee.Usr,
                OfficeId = dataBaseEmployee.R1
            };
        }

        public static Customer ToCustomer(this Actor dataBaseCustomer)
        {
            if (dataBaseCustomer == null) return null;

            return new Customer
            {
                CustomerId = dataBaseCustomer.CustNo,
                Adress = dataBaseCustomer.Ad1,
                City = dataBaseCustomer.PArea,
                Email = dataBaseCustomer.MailAd,
                Name = dataBaseCustomer.Nm,
                PhoneNumber = dataBaseCustomer.Phone,
                PostalCode = dataBaseCustomer.PNo,
            };
        }

        public static Activity ToContract(this Prod dataBaseActivity)
        {
            if (dataBaseActivity == null) return null;
            return new Activity
            {
                Id = dataBaseActivity.ProdNo,
                Description = dataBaseActivity.Descr,

            };
        }

        public static Service ToContract(this R8 dataBaseService)
        {
            if (dataBaseService == null) return null;
            return new Service
            {
                Id = dataBaseService.RNo,
                Description = dataBaseService.Nm
            };
        }

        public static Project ToEntitie(this R9 dataBaseProject)
        {
            if (dataBaseProject == null) return null;
            return new Project
            {
                Id = dataBaseProject.RNo,
                Name = dataBaseProject.Nm
            };
        }

        public static Actor ToEntitie(this Employee employee)
        {
            return new Actor
            {
                EmpNo = employee.EmployeeId,
                Ad1 = employee.Adress,
                PArea = employee.City,
                MailAd = employee.Email,
                Phone = employee.PhoneNumber,
                PNo = employee.PostalCode,
                Usr = employee.Username,
                Nm = employee.Name
            };
        }

        public static WorkText ToTypeOfWorkText(this Txt dataBaseWorkText)
        {
            return new WorkText
            {
                Id = dataBaseWorkText.TxtNo,
                Text = dataBaseWorkText.Txt1,
                GroupId = dataBaseWorkText.TxtTp
            };
        }

        #endregion
    }
}
