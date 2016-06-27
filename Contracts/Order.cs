namespace Contracts
{
    public class Order
    {
        public int OrderId { get; set; }

        public Customer Customer { get; set; }

        public Agreement Agreement { get; set; }

        //public TypeOfWorkText WorkText { get; set; }   
    }
}