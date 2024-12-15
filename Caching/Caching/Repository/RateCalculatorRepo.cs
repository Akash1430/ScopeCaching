using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caching.BL
{
    abstract class RateCalculatorRepo
    {
        protected decimal CalculateTotal(decimal total, decimal tax) 
        {
            return total * (1*tax); 
        }

        protected decimal SubTotal(decimal total, decimal tax)
        {
            return total - (1 * tax);
        }
        protected string GetJobRatesAndValue(string jobName, int anunalSalary, int hoursWorked)
        {
            return $"{jobName} {anunalSalary} {hoursWorked}";
        }
        protected async Task<List<string>> GetAllJobs()
        {
            return await Task.FromResult(new List<string>() { "Job1","Job2"});
        }
    }
}
