using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class CustomerHandler
    {
        private UnitOfWork _uow;

        public CustomerHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public Customer Get(int id)
        {
            var customer = _uow.ActorRepository.Get(x => x.CustNo == id).FirstOrDefault();

            return customer == null
                ? null
                : customer.ToCustomer();
        }

        public List<Customer> Get()
        {
            return _uow.ActorRepository.Get(x => x.CustNo != 0).ToCustomers();
        }
    }
}