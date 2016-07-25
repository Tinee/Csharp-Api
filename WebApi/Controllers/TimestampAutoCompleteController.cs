using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contracts;
using Business_Logic.Handlers;

namespace WebApi.Controllers
{
    public class TimestampAutoCompleteController : ApiController
    {
        TimestampAutoCompleteHandler _timestampAutoCompleteHandler;

        public TimestampAutoCompleteController()
        {
            _timestampAutoCompleteHandler = new TimestampAutoCompleteHandler();
        }

        [Route("api/TimestampAutoComplete/")]
        public TimestampingViewModel Get()
        {
            return _timestampAutoCompleteHandler.GetAutoCompleteValues();
        }

        [Route("api/TimestampAutoComplete/{contractId}")]
        public List<Service> Get(int contractId)
        {
            return _timestampAutoCompleteHandler.GetServices(contractId);
        }

        [Route("api/TimestampAutoComplete/Agreements/{customerId}")]
        public List<Agreement> GetAgreements(int customerId)
        {
            return _timestampAutoCompleteHandler.GetAgreements(customerId);
        }
    }
}
