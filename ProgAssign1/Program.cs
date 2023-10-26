using System.Diagnostics;

using Assignment1_dirwalker;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            Stopwatch stopwatch = new Stopwatch();

            string inputDirPath = "/Users/dv/Documents/Class Projects/MCDA5510/MCDA5510_Assignments/MCDA5510_ASSigNment1/MCDA5510_ASSigNment1/Sample Data";
            stopwatch.Start();

            Console.Write("Traversing the Directories for CSV Files...");

            using (StreamWriter logFile = new StreamWriter(DirectoryTraverser.logFilePath, true))
            {
                var fileLineTxt = new List<String>();
                TextWriter originalConsoleOut = Console.Out;
                Console.SetOut(logFile);
                Console.WriteLine("Process Has Started");

                //Generate Headers
                CSVParser.processFilesAndCopy(inputDirPath);

                Console.WriteLine("Process is done");

                stopwatch.Stop();
                TimeSpan elapsed = stopwatch.Elapsed;
                Console.WriteLine("Total execution time: " + elapsed);
                Console.WriteLine("Total number of valid rows: " + Counter.GetProcessCount());
                Console.WriteLine("Total number of skipped rows: " + Counter.GetSkipCount());
                Console.SetOut(originalConsoleOut);
                Console.WriteLine("All the CSV Files been processed.");
                Console.WriteLine("Location of Logs File: " + DirectoryTraverser.logFilePath);
                Console.WriteLine("Location of the Output File: " + DirectoryTraverser.outputFilePath);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Exception Occured" + " " + ex.Message);
        }
    }
}