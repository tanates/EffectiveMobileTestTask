using EffectiveMobileModelDTO.Model;
using EffectiveMobileTestTask.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobileLogicFile
{
    public class FileServisec
    {
        private readonly IFileRepository _fileRepository;

        public FileServisec(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public string ReadWriteLogFile(Options options ,FilterData filterData)
        {

            try
            {
        

                var readFileList  =  _fileRepository.ReadLogFile(options.FileLog);
             
                var filterList = _fileRepository.FilterLogFile(readFileList, options.AddressStart, options.AddressMask, filterData);
        
                var resultAddFile = _fileRepository.CreatNewLogFile(options.FileOutput, filterList);
                return resultAddFile;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка при проверке параметров: {ex.Message}");
                return null;
            }
        }

   

     
    }
}
