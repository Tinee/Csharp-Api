using System.Collections.Generic;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class OfficeHandler
    {
        private readonly UnitOfWork _uow;
        public OfficeHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public Office Get(int id)
        {
            return _uow.OfficeRepository.Get(id).ToContract();
        }

        public IEnumerable<Office> Get()
        {
            return _uow.OfficeRepository.GetAll().ToContracts();
        }
    }
}
