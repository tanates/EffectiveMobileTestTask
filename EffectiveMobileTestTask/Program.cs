using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EffectiveMobileLogicFile;
using EffectiveMobileModelDTO.Model;
using EffectiveMobileModelDTO.Repository;
using EffectiveMobileTestTask.Interface;
namespace EffectiveMobileTestTask
{
    internal class Program
    {
        
       
        static void Main(string[] args)
        {
            IFileRepository iFileRepository = new WorkingWithAFile();
            FileServisec fileServisec = new FileServisec(iFileRepository);
            FilterData filterData = new FilterData();
            IOptionsRepository optionsRepository = new OptionsRepository();
            var options = optionsRepository.SetOptionsInConsole(args);
            ParameterValidator.ValidateParameters(options);
            filterData.timeStartParsed = DateTime.ParseExact(options.TimeStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            filterData.timeEndParsed = DateTime.ParseExact(options.TimeEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            fileServisec.ReadWriteLogFile(options, filterData);


         }
    }
}
