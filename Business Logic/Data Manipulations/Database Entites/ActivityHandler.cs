using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.UnitOfWork;
using Mappers;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class ActivityHandler
    {

        private readonly UnitOfWork _uow;

        public ActivityHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public Activity Get(string id)
        {
            var databaseActivity = _uow.ActivityRepository.Get(x => x.ProdNo == id).FirstOrDefault();

            return databaseActivity == null
                ? null
                : databaseActivity.ToContract();
        }

        public List<Activity> Get()
        {
            return _uow.ActivityRepository.GetAll().ToContracts();
        }
    }
}
