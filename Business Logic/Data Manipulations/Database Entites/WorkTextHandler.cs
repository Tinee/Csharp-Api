using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class WorkTextHandler
    {
        private readonly UnitOfWork _uow;

        public WorkTextHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public List<WorkText> Get()
        {
            return _uow.TypeOfWorkTextRepository.GetAll().Where(x => x.Lang == 752 && x.TxtTp == 72).ToContracts();
        }
    }
}
