# Assingment 1 : Directory Walker and CSV Processor

# Location

-> The Code is located under the ProgAssign1 Directory.

-> The Logs are stored in the ProgAssign1/logs/logs.txt file.

-> The Output Csv is stored in the ProgAssign1/Output/output.csv file.

# Introduction
This C# program is designed to traverse a directory structure containing CSV files and store the data in a single CSV file. The CSV files are expected to contain customer information with specific data columns.

# Program Features

-> Directory Traversal: The program recursively traverses the specified directory structure.

-> CSV Manager: The program can read and write in the CSV files. CSVHelper Library is used to write the Data in the CSV file.

-> Logging: The program utilizes logging to capture informational messages and all possible checked exceptions.

-> Data Validation: It skips lines with incomplete records, rows with unmatched datatypes and failing validation for each column and logs them as skipped rows.

-> Logging Output: The program logs the total execution time, the total number of valid rows, and the total number of skipped rows.


-> Data Columns: The program expects CSV files with the following data columns:
- First Name
- Last Name
- Street Number
- Street
- City
- Province
- Country
- Postal Code
- Phone Number
- Email Address
- Date (yyyy/mm/dd) extracted from the directory structure

# Notes
Change the rootDir variable in program.cs and logFilePath and outputFilePath in DirectoryTraverser.cs to your local directory.
This program was coded in a macbook. In order to ensure compatibility with Windows, if you intend to run this program on a Windows machine, please make the following modification:
Replace the forward slashes '/' with backslashes '\' in the regexPattern variable located in the CSVManager.cs file."




# Assignment 0

This Assignment was given as the part of MCDA5510 Lecture held on 6th September 2023. The Purpose of this assignment is to identify 2 other people with common interests and list them with a fun fact about them after giving a brief Introduction about myself.




## About Me

My name is Deepakk Vignesh Jayamohan. My A# is A00473427. I'm from Chennai,India. I completed my four-year bachelor's degree back in 2020. After that I worked as a Software Developer for 3 years specializing in frontend before joining SMU.

you can reach out to me by emailing me to deepakk.vignesh.jayamohan@smu.ca.

## Common Topic

The common topic that bought us together is our love for the sport of cricket. In countries like India and Pakistan cricket is a like a religion. The Google defines cricket as a game played with a ball and bat by two sides of usually 11 players each on a large field centering upon two wickets each defended by a batsman. But, there is more to it. The definition misses the memories, the emotions, and the pride when our respective national teams step into the field. And those factors mixed with our blood is what made us the person we are, and we will be.

## People that I share common Interests with

The following are the team members that share a common interest with me.

-> Umair Habib - Umair is from Pakistan. he too has worked as a software developer before specializing in backend. His favorite cricketer is Shahid Afridi.

-> Noman Shafi - Noman is from Pakistan. Like the rest of us Noman has worked as a software developer too with only difference being that he is a full-stack developer. His favorite cricketer is Shoaib aktar.


