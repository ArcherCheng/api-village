
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services;

public interface IAk0KeyCodeService : IApiBaseService
{
     Task<IEnumerable<KeyValuePair<string,string>>> GetKeyValueListAsync(string group);
}

