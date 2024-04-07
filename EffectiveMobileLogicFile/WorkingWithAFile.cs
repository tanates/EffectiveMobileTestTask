using EffectiveMobileModelDTO.Model;
using EffectiveMobileTestTask.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EffectiveMobileLogicFile
{
    public class WorkingWithAFile : IFileRepository
    {
        public string CreatNewLogFile(string outputFilePath, List<FilterLogOut> logEntries)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (var entry in logEntries)
                    {
                        writer.WriteLine($"{entry.IpAddress}: {entry.Count}");
                    }
                }
            }
            catch (IOException ex)
            {
              
                return $"Ошибка при записи выходного файла: {ex.Message}";
            }

            return "Обработка завершена.";
        }

        public List<FilterLogOut> FilterLogFile(List<LogEntry> logEntries , string addressStart ,string addressMask , FilterData filterData)
        {
            List<LogEntry> filteredLogEntries = logEntries;
            if (!string.IsNullOrEmpty(addressStart))
            {
                uint addressStartUint = ParameterValidator.IpAddressToUint(addressStart);

                if (!string.IsNullOrEmpty(addressMask))
                {
                    uint addressMaskUint = ParameterValidator.IpAddressToUint(addressMask);

                    filteredLogEntries = filteredLogEntries.Where(entry => (entry.IpAddressUint & addressMaskUint) == addressStartUint).ToList();
                }
                else
                {
                    filteredLogEntries = filteredLogEntries.Where(entry => entry.IpAddressUint >= addressStartUint).ToList();
                }
            }

            // Фильтрация логов по временному интервалу
            if (filterData.timeStartParsed != null)
            {
                filteredLogEntries = filteredLogEntries.Where(entry => entry.Timestamp >= filterData.timeStartParsed).ToList();
            }

            var groupedLogEntries = filteredLogEntries.GroupBy(entry => entry.IpAddress).Select(group => new { IpAddress = group.Key, Count = group.Count() });

            return groupedLogEntries.Select(entry => new FilterLogOut
            {
                IpAddress = entry.IpAddress,
                Count = entry.Count
            }).ToList();

        }

        public List<LogEntry> ReadLogFile( string filePath)
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            try
            {
              using  (StreamReader reader =  new StreamReader(filePath))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if(parts.Length != 4 )
                        {
                            Console.WriteLine($"Некорректная строка в файле журнала: {line}");
                            continue;
                        }

                        string ipAddress = parts[0];
                        string time = parts[1] + ":" + parts[2] + ":" + parts[3];
                        time = time.Trim();
                        DateTime timestamp;
                        try
                        {
                            timestamp = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Некорректный формат времени в строке журнала: {line}");
                            continue;
                        }
                        logEntries.Add(new LogEntry
                        {
                            IpAddress = ipAddress,
                            Timestamp = timestamp,
                            IpAddressUint = ParameterValidator.IpAddressToUint(ipAddress)
                        });
                    }
                }
                return logEntries;
            }
            catch (FileNotFoundException fileEx)
            {
                Console.WriteLine($"Ошибка при чтении файла журнала: {fileEx.Message}");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при чтении файла журнала: {ex.Message}");
                return null;
            }
        }

    }
   

}

