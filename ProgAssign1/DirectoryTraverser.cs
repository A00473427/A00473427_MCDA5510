namespace Assignment1_dirwalker
{
    class DirectoryTraverser
    {
        public static string logFilePath = "/Users/dv/Documents/Class Projects/MCDA5510/MCDA5510_Assignments/MCDA5510_ASSigNment1/MCDA5510_ASSigNment1/logs/logs.txt";
        public static string outputFilePath = "/Users/dv/Documents/Class Projects/MCDA5510/MCDA5510_Assignments/MCDA5510_ASSigNment1/MCDA5510_ASSigNment1/output/output.csv";

        public static void createDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
            }
        }

        public static string getLogFilePath()
        {
            return logFilePath;
        }

        public static string getOutputFilePath()
        {
            return outputFilePath;
        }

        public static void TraverseDirectory(string directory, List<string> csvFileList)
        {
            string[] subDirectories = Directory.GetDirectories(directory);
            if (subDirectories.Length > 0)
            {
                foreach (string subDirectory in subDirectories)
                {
                    Console.WriteLine("Now Goin through the directory"+subDirectory); 
                    TraverseDirectory(subDirectory, csvFileList);
                }
            }

            GetDirectoryFiles(directory, csvFileList);
        }

        public static void GetDirectoryFiles(string directory, List<string> csvFileList)
        {
            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                if (Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Parsing CSV File in " + file);
                    csvFileList.Add(file);
                }
            }
        }

        public static string setRootDirectory(string dirPath)
        {
            Directory.SetCurrentDirectory(dirPath);
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Directory changed to " + currentDirectory);
            return currentDirectory;
        }
    }
}
