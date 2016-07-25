using System;
using System.Collections.Generic;
using System.Linq;
using Business_Logic.Helpers;
using Contracts;
using DataService;
using DataService.UnitOfWork;

namespace Business_Logic.Data_Manipulations.Database_Entites
{
    public class TimestampHandler
    {
        private readonly List<Agr> _databaseTimestamps = new List<Agr>();
        private List<int> _intDates = new List<int>();
        private readonly List<Timestamp> _timestamps = new List<Timestamp>();
        private readonly EmployeeHandler _employeeHandler = new EmployeeHandler();
        private readonly ActivityHandler _activityHandler = new ActivityHandler();
        private readonly AgreementHandler _agreementHandler = new AgreementHandler();
        private readonly ProjectHandler _projectHandler = new ProjectHandler();
        private readonly WorkTextHandler _workTextHandler = new WorkTextHandler();
        private readonly CustomerHandler _customerHandler = new CustomerHandler();

        private readonly UnitOfWork _uow;

        public TimestampHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        private List<Agr> GetDatabaseTimestamps(List<DateTime> dates, int employeeId)
        {
            //Converts the dates to ints because the database have the date column as int and not date.
            _intDates = dates.ToInts();

            /*Loops the intDates and adds all the
             * timestamps in a list that I call _databaseTimestamps,
             * it only adds the Timestamps who has a certain
             * employeeId and a certain date*/
            _intDates.ForEach(date => _databaseTimestamps
                .AddRange(_uow.TimeStampsRepository
                .Get(filter: x => x.CreDt == date && x.EmpNo == employeeId)
                .ToList()));

            return _databaseTimestamps;
        }

        private List<Timestamp> ConvertDatabaseEntitiesToTimestamps(List<Agr> databaseTimestamps,int employeeId)
        {
            var activities = _activityHandler.Get();
            var employee = _employeeHandler.Get(employeeId);
            var projects = _projectHandler.Get();
            var workTexts = _workTextHandler.Get();


            databaseTimestamps.ForEach(item => _timestamps.Add(new Timestamp
            {
                Id = item.ActNo,
                Employee = employee,
                ClockFrom = item.FrTm,
                ClockTo = item.ToTm,
                DateFrom = item.FrDt.ConvertIntToDatetime(),
                DateTo = item.ToDt.ConvertIntToDatetime(),
                DefaultActivity = Convert.ToBoolean(item.Pri),
                Description = item.Descr,
                InternDescription = item.Descr2,
                Activity = activities.FirstOrDefault(x => x.Id == item.ProdNo),
                Agreement = _agreementHandler.Get(item.R5),
                Project = projects.FirstOrDefault(x => x.Id == item.R9),
                TotalTimeSpend = item.NoReg.MinutesToHours(),
                //This is insane, but the database is wierd, get use to it :)
                IsDebiting = item.ProdPrGr.ReverseBoolean(),
                Wage = Convert.ToBoolean(item.AdWage1),
                //OutLay = Convert.ToBoolean(item.ProdPrG2),
                InvoicedTime = item.NoInvoAb.MinutesToHours(),
                Occupation = workTexts.FirstOrDefault(x => x.Id == item.ProdPrG3),
                Customer = _customerHandler.Get(item.CustNo)
            }));

            return _timestamps;
        }

        public List<Timestamp> GetTimestampFromEmployeeAndDates(List<DateTime> dates, int employeeId)
        {
            //Gets the raw data from the database.
            var databaseTimestamps = GetDatabaseTimestamps(dates, employeeId);

            //Inserts the raw database data and gets cleen data that we want on the client.
            return ConvertDatabaseEntitiesToTimestamps(databaseTimestamps, employeeId);
        }


    }
}
