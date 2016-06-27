using System.Collections.Generic;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class AgreementHandler
    {
        private readonly UnitOfWork _uow;

        public AgreementHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public Agreement Get(int id)
        {
            return _uow.AgreementRepository.Get(id).ToContract();
        }

        public List<Agreement> Get()
        {
            return _uow.AgreementRepository.GetAll().ToContracts();
        }
    }
}

