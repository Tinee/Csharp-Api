using System;

namespace Contracts
{
    public class Timestamp
    {
        public int Id { get; set; }


        public string Description { get; set; }

        public string InternDescription { get; set; }

        public int Tax { get; set; }

        public bool Wage { get; set; }

        public bool IsDebiting { get; set; }

        public bool DefaultActivity { get; set; }

        public bool OutLay { get; set; }

        public Order Order { get; set; }

        public Employee Employee { get; set; }

        public Activity Activity { get; set; }

        public Service Service { get; set; }

        public Customer Customer { get; set; }

        public Agreement Agreement { get; set; }

        public Project Project { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int ClockFrom { get; set; }

        public int ClockTo { get; set; }

        public decimal TotalTimeSpend { get; set; }

        public WorkText WorkText { get; set; }
        public decimal InvoicedTime { get; set; }

    }
}
