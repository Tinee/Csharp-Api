using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class TimestampingViewModel
    {
        public List<Activity> Activities { get; set; }

        public List<Customer> Customer { get; set; }

        public List<Agreement> Agreement { get; set; }

        public List<Tax> Taxes { get; set; }

        public List<Service> Services { get; set; }

        public List<Project> Projects { get; set; }

        public List<WorkText> Occupations { get; set; }
    }
}
