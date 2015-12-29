using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou {
    public interface IGeolocatorService {
        Task<string> GetCityNameAsync();
    }
}
