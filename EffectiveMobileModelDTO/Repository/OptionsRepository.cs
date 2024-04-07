using EffectiveMobileModelDTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobileModelDTO.Repository
{
    public class OptionsRepository : IOptionsRepository
    {
        public  Options SetOptionsInConsole(string[] args)

        {
            var options = new Options();
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--file-log":
                        options.FileLog = args[++i];
                        break;
                    case "--file-output":
                        options.FileOutput = args[++i];
                        break;
                    case "--address-start":
                        options.AddressStart = args[++i];
                        break;
                    case "--address-mask":
                        options.AddressMask = args[++i];
                        break;
                    case "--time-start":
                        options.TimeStart = args[++i];
                        break;
                    case "--time-end":
                        options.TimeEnd = args[++i];
                        break;

                }
            }
             List <Options> optionsList = new List<Options>();
            optionsList.Add(options);
            return options;
        }
      
       

      
   

        List<Options> IOptionsRepository.SetOptionsInConfig()
        {
            throw new NotImplementedException();
        }
    }
}
