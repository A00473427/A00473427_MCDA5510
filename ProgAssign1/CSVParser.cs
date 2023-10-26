using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Assignment1_dirwalker
{
    class CSVParser
    {
        private static List<Attributes> getAllCSVrecords(string[] inputCsvFiles, List<string> csvDates)
        {
            var csvRows = new List<Attributes>();
            foreach (var inputCsvFile in inputCsvFiles)
            {
                int flag = 0;
                using (var reader = new StreamReader(inputCsvFile))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null
                }))
                {
                    List<Attributes> records = new List<Attributes>();

                    try
                    {
                        flag = 0;
                        records = csv.GetRecords<Attributes>().ToList();
                    }
                    catch (CsvHelper.HeaderValidationException ex)
                    {
                        flag = 1;
                        Console.WriteLine("Exception Occured: Header Validation: " + ex.Message);
                    }
                    catch (CsvHelper.MissingFieldException ex)
                    {
                        flag = 1;
                        Console.WriteLine("Exception Occured: Missing Field: " + ex.Message);
                    }

                    if (flag == 0)
                    {
                        var filteredRecords = records.Where(record => {
                            var isInvalid = IsValidRow(record);
                            if (!isInvalid)
                            {
                                csvDates.Add(extractDates(inputCsvFile));
                            }
                            return !isInvalid;

                        }).ToList();

                        csvRows.AddRange(filteredRecords);
                    }
                }
            }
            return csvRows;
        }

        private static void writeToOutputCSV(List<Attributes> CombinedCsvRecords, List<string> csvDates)
        {
            using (var writer = new StreamWriter(DirectoryTraverser.outputFilePath, true))
            using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            }))
            {
                int i = 0;
                foreach (var csvRecord in CombinedCsvRecords)
                {
                    csvWriter.WriteRecord(csvRecord);
                    csvWriter.WriteField(csvDates[i]);
                    i++;
                    csvWriter.NextRecord();
                    Counter.IncrementProcessCount();
                }
            }
        }

        public static void processFilesAndCopy(string inputDirPath)
        {
            var inputCsvFiles = new List<string>();
            string currDir = DirectoryTraverser.setRootDirectory(inputDirPath);
            DirectoryTraverser.TraverseDirectory(currDir, inputCsvFiles);
            var csvDates = new List<string>();
            var csvRecords = getAllCSVrecords(inputCsvFiles.ToArray(), csvDates);
            CSVParser.generateHeaders();
            writeToOutputCSV(csvRecords, csvDates);
        }

        public static void generateHeaders()
        {
            using (var writer = new StreamWriter(DirectoryTraverser.logFilePath, true))
            using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvWriter.WriteField("First Name");
                csvWriter.WriteField("Last Name");
                csvWriter.WriteField("Street Number");
                csvWriter.WriteField("Street");
                csvWriter.WriteField("City");
                csvWriter.WriteField("Province");
                csvWriter.WriteField("Postal Code");
                csvWriter.WriteField("Country");
                csvWriter.WriteField("Phone Number");
                csvWriter.WriteField("Email Address");
                csvWriter.WriteField("Date");
                csvWriter.NextRecord();
            }
        }

        private static bool IsValidRow(Attributes record)
        {
            bool isValid = record.GetType().GetProperties().Any(property =>
            {
                var value = property.GetValue(record);
                if (value == null || (value is string str && string.IsNullOrEmpty(str)))
                {
                    return true;
                }

                if (property.Name == "FirstName" && value is string firstName)
                {
                    if (!Regex.IsMatch(firstName, "^[A-Za-z'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "LastName" && value is string lastName)
                {
                    if (!Regex.IsMatch(lastName, "^[A-Za-z\\s'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "Street" && value is string Street)
                {
                    if (!Regex.IsMatch(Street, "^[A-Za-z0-9\\s.'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "City" && value is string City)
                {
                    if (!Regex.IsMatch(City, "^[A-Za-z\\s.'-]+$|Saint-JÃ©rÃ´me|Trois-RiviÃ¨res|LÃ©vis"))
                    {
                        return true;
                    }
                }

                if (property.Name == "Province" && value is string Province)
                {
                    if (!Regex.IsMatch(Province, "^[A-Za-z\\s]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "Country" && value is string Country)

                {
                    if (!Regex.IsMatch(Country, "^[A-Za-z\\s'-]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "StreetNumber" && value is string StreetNumber)
                {
                    if (!Regex.IsMatch(StreetNumber, "^[0-9A-Za-z ]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "PostalCode" && value is string PostalCode)
                {
                    if (!Regex.IsMatch(PostalCode, @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d *$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "PhoneNumber" && value is string PhoneNumber)
                {
                    if (!Regex.IsMatch(PhoneNumber, "^[0-9]+$"))
                    {
                        return true;
                    }
                }

                if (property.Name == "EmailAddress" && value is string EmailAddress)
                {
                    if (!Regex.IsMatch(EmailAddress, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                    {
                        return true;
                    }
                }

                return false;
            });

            if (isValid)
            {
                Counter.IncrementSkipCount();
                Console.WriteLine($"SKIPPED ROW ALERT - First Name: {record.FirstName}, Last Name: {record.LastName}, Street Number: {record.StreetNumber}, Street: {record.Street}, City: {record.City}, Province: {record.Province}, Postal Code: {record.PostalCode}, Country: {record.Country}, Phone Number: {record.PhoneNumber}, Email Address: {record.EmailAddress}");
            }

            return isValid;
        }
        public static string extractDates(string path)
        {
            string regexPattern = @"\b(\d{4}/\d{1,2}/\d{1,2})\b";
            Match match = Regex.Match(path, regexPattern);
            string extractedDate = match.Groups[1].Value;

            return extractedDate;
        }
    }
}
