using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class ProjectHandler
    {
        private readonly UnitOfWork _uow;

        public ProjectHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public Project Get(string id)
        {
            return _uow.ProjectRepository.Get(x => x.RNo == id).FirstOrDefault().ToEntitie();
        }

        public List<Project> Get()
        {
            return _uow.ProjectRepository.Get().ToContracts();
        }
    }
}
