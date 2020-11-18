using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfParallalProcessingTask
{
    public class BusinessLogic
    {

        public async Task<string> RunFileProcessLogicsParallel(string fileName)
        {
            Console.WriteLine("Started -ProcessCSVFile In BLogic. File:"+ fileName);
            string ProcessResult = "No File";
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                ProcessResult = fileName;

            }
            catch (Exception ex)
            {
                ProcessResult = "Error_" + fileName;
            }
            Console.WriteLine("End -ProcessCSVFile In BLogic. File:" + fileName);
            return await Task.FromResult(ProcessResult);
        }
    }
}
