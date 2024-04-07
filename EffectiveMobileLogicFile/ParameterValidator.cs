using EffectiveMobileModelDTO.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobileLogicFile
{
    public class ParameterValidator
    {
        public static void ValidateParameters(Options options)
        {
            FilterData filterData = new FilterData();
            // Проверка корректности logFilePath
            if (string.IsNullOrEmpty(options.FileLog))
            {
                throw new ArgumentException("Не указан путь к файлу журнала.", nameof(options.FileLog));
            }

            // Проверка корректности outputFilePath
            if (string.IsNullOrEmpty(options.FileOutput))
            {
                throw new ArgumentException("Не указан путь к выходному файлу.", nameof(options.FileOutput));
            }

            // Проверка корректности addressStart
            if (!string.IsNullOrEmpty(options.AddressStart))
            {
                try
                {
                    IpAddressToUint(options.AddressStart);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Некорректный IP-адрес.", nameof(options.AddressStart));
                }
            }

            // Проверка корректности addressMask
            if (!string.IsNullOrEmpty(options.AddressMask ))
            {
                if (string.IsNullOrEmpty(options.AddressMask))
                {
                    throw new ArgumentException("Нельзя использовать параметр address-mask без параметра address-start.", nameof(options.AddressMask));
                }

                try
                {
                    IpAddressToUint(options.AddressMask);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Некорректная маска подсети.", nameof(options.AddressMask));
                }
            }

            // Проверка корректности timeStart
            if (!string.IsNullOrEmpty(options.TimeStart))
            {
                try
                {
                    filterData.timeStartParsed= DateTime.ParseExact(options.TimeStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Некорректный формат даты начала временного интервала.", nameof(options.TimeStart));
                }
            }

            // Проверка корректности timeEnd
            if (!string.IsNullOrEmpty(options.TimeEnd))
            {
                try
                {
                  filterData.timeEndParsed= DateTime.ParseExact(options.TimeEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                }
                catch (FormatException)
                {
                    throw new ArgumentException("Некорректный формат даты окончания временного интервала.", nameof(options.TimeEnd));
                }
            }
        }

        public static uint IpAddressToUint(string ipAddress)
        {
            string[] parts = ipAddress.Split('.');
            if (parts.Length != 4)
            {
                throw new ArgumentException("Некорректный IP-адрес.", nameof(ipAddress));
            }

            uint ipAddressUint = 0;
            for (int i = 0; i < 4; i++)
            {
                ipAddressUint |= (uint.Parse(parts[i]) << (8 * i));
            }

            return ipAddressUint;
        }
    }

}
