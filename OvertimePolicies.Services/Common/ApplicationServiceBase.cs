using OvertimePolicies.WebApp.Common.DatetimeHelper;

namespace OvertimePolicies.Services.Common
{
    public class ApplicationServiceBase
    {
        public IDateTimeHelper _dateTimeHelper;
        public ApplicationServiceBase(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }
    }
}