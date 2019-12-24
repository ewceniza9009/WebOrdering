using System.Linq;

namespace AccountMateWebOrder.Services
{
    public static class CompanyService
    {
        public static string CompanyName 
        {
            get 
            {
                return new Data.AMSysDataContext().Companies.FirstOrDefault().Name;
            }
        }
    }
}