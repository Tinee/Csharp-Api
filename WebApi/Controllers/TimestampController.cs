using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Business_Logic.Data_Manipulations.Database_Entites;
using Business_Logic.Handlers;
using Contracts;
using Dummy_Data;
using Enums;

namespace WebApi.Controllers
{
    public class TimestampController : ApiController
    {
        private readonly DataService _dummyDataService = new DataService();
        private readonly DateHandler _dateHandler = new DateHandler();
        private readonly TimestampHandler _timestampHandler = new TimestampHandler();


        [Route("api/timestamp/month/{month}/{employeeId}")]
        public IEnumerable<Timestamp> Get(Month month, int employeeId)
        {
            var dummyTimestamps = _dummyDataService.GetDummyTimestamps()
                .Where(p => p.DateFrom.Month == Convert.ToInt32(month))
                .ToList();

            var asd = _dummyDataService.GetDummyTimestamps()
                .Where(p => p.DateFrom.Month == Convert.ToInt32(month))
                .ToList();

            return dummyTimestamps;
        }

        [Route("api/timestamp/day/{date}")]
        public IEnumerable<Timestamp> Get(DateTime date, int employeeId)
        {
            var dummyTimestamps = _dummyDataService.GetDummyTimestamps()
                .Where(p => p.DateFrom == date)
                .ToList();

            return dummyTimestamps;
        }

        [Route("api/timestamp/between/{startDate}/{toDate}/{employeeId}")]
        public IEnumerable<Timestamp> Get(DateTime startDate, DateTime toDate, int employeeId)
        {
            return _timestampHandler
                .GetTimestampFromEmployeeAndDates(_dateHandler.DateRange(startDate, toDate), employeeId);
        }

        public IHttpActionResult Post([FromBody] Timestamp timeStamp)
        {
            //TODO 
            //Fix parameter on OldDbHandler
            var oldDbHandler = new InsertTimestampHandler("marcar");

            oldDbHandler.Insert(100359, 15, 2000, false, "010", false, 0, 1, 1, false, "Test Semester", "Test Description2", true, 20160609, 20160609,1400,1500,"","","Interna möten",0);

            return Ok();
        }
    }
}
