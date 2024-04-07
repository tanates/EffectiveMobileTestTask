using EffectiveMobileModelDTO.Model;
using System.Collections.Generic;

namespace EffectiveMobileModelDTO.Repository
{
    public interface IOptionsRepository
    {
        Options SetOptionsInConsole(string[] args);
        List<Options> SetOptionsInConfig();

        
    }
}