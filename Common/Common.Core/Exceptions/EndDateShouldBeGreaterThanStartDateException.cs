namespace Common.Core.Exceptions
{
    public class EndDateShouldBeGreaterThanStartDateException : BusinessException
    {
        public EndDateShouldBeGreaterThanStartDateException():base("-1", "EndDate should be greater than StartDate")
        {
            
        }
    }
}
