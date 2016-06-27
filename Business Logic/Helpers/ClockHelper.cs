namespace Business_Logic.Helpers
{
    public static class ClockHelper
    {

        public static decimal MinutesToHours(this decimal minutes)
        {
            return minutes / 60;
        }

        public static decimal HoursToMinutes(this decimal hours)
        {
            return hours * 60;
        }
    }
}
