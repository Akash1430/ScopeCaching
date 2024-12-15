using Caching.BL;
using System;
using System.Threading.Tasks;

namespace Caching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RateCalculatorRepoProxy rateCalculator1 = new RateCalculatorRepoProxy();
            rateCalculator1.CalculateTotal(1, 2);
            rateCalculator1.CalculateTotal(1, 2);
            rateCalculator1.CalculateTotal(1, 2);
           var x =  Task.Run(()=> rateCalculator1.GetAllJobs()).Result;
            var y  =  rateCalculator1.GetAllJobs().GetAwaiter().GetResult();

            RateCalculatorRepoProxy rateCalculator2 = new RateCalculatorRepoProxy();
            rateCalculator2.CalculateTotal(1, 2);
            rateCalculator2.CalculateTotal(1, 2);
            rateCalculator2.CalculateTotal(1, 2);
            rateCalculator2.GetJobDetails("AA", 20000,20);

            Console.ReadLine();
        }
    }
}
