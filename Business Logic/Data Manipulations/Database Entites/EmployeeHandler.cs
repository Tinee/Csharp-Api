using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class EmployeeHandler
    {
        private readonly UnitOfWork _uow;

        public EmployeeHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public Employee Get(int id)
        {
            return _uow.ActorRepository.Get(filter: x => x.EmpNo == id).FirstOrDefault().ToEmployee();
        }

        public List<Employee> Get()
        {
            return _uow.ActorRepository.GetAll().ToEmployees();
        }
    }
}
