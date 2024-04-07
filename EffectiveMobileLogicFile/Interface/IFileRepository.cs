using EffectiveMobileModelDTO.Model;
using EffectiveMobileModelDTO.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobileTestTask.Interface
{
    public interface IFileRepository
    {
        List<LogEntry> ReadLogFile(string filePath);
        List<FilterLogOut> FilterLogFile(List<LogEntry> logEntries, string addressStart, string addressMask, FilterData filterData);
        string  CreatNewLogFile(string filePath , List<FilterLogOut> logEntries); 

    }
}
