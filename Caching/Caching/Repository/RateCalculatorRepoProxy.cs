using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Caching.BL
{
    internal class RateCalculatorRepoProxy : RateCalculatorRepo
    {
        private readonly Dictionary<string, object> _cache;

        public RateCalculatorRepoProxy() 
        {
            _cache = new Dictionary<string, object>();
        }

        public decimal CalculateTotal(decimal total, decimal tax, bool useCaching = true)
        {
            string paramValues = $"{total} {tax}";
            return useCaching ? CallGetMethodWithCaching(
                calculationMethod => base.CalculateTotal(total, tax),
                paramValues) : base.CalculateTotal(total, tax);
        }

        public decimal SubTotal(decimal total, decimal tax, bool useCaching = true)
        {
            string paramValues = $"{total} {tax}";
            return useCaching ? CallGetMethodWithCaching(
                calculationMethod => base.SubTotal(total, tax),
                paramValues) : base.SubTotal(total, tax);
        }

        public string GetJobDetails(string jobName, int annualSalary, int hoursWorked, bool useCaching = true)
        {
            string paramValues = $"{annualSalary} {jobName}  {hoursWorked}";
            return useCaching ?  CallGetMethodWithCaching(
                calculationMethod => base.GetJobRatesAndValue(jobName, annualSalary, hoursWorked),
                paramValues) : base.GetJobRatesAndValue(jobName, annualSalary, hoursWorked);
        }

        public async Task<List<string>> GetAllJobs(bool useCaching = true)
        {
            string paramValues = $"";
            return useCaching ? await CallGetMethodWithCaching(
                calculationMethod => base.GetAllJobs(),
                paramValues) : await base.GetAllJobs();
        }

        private T CallGetMethodWithCaching<T>(Func<string, T> calculationMethod, string paramValues="", [CallerMemberName] string methodName = "")
        {
            // Generate a cache key using the method name and parameters
            var cacheKey = GetCacheKey(paramValues, methodName);

            if (_cache.ContainsKey(cacheKey))
            {
                Console.WriteLine("Cached value");
                return (T)_cache[cacheKey];
            }

            // If not cached, invoke the method and store the result
            T result = calculationMethod("Invoke Method");
            _cache.Add(cacheKey, result);

            Console.WriteLine("Not cached value");
            return result;
        }

        private string GetCacheKey(string paramValues, string methodName)
        {
            return $"{methodName}{paramValues}";
        }

    }
}
