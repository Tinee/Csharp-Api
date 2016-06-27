namespace Business_Logic.Helpers
{
    public static class BooleanHelper
    {
        public static bool ReverseBoolean(this int oneOrZero)
        {
            /*Returns reverse boolean, if a 1 comes in it return false, and if zero comes in it returns true...
             * Blame the database not me.*/
            return oneOrZero != 1;
        }
    }
}