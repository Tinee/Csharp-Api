using System;
using System.Collections.Generic;
using System.Linq;

namespace Business_Logic.Handlers
{
    public class DateHandler
    {

        public List<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d)).ToList();
        }
    }
}
