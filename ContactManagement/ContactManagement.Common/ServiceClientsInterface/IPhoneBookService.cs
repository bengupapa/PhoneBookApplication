using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement.Common.ServiceClientsInterface
{
    public interface IPhoneBookService: IService
    {
        Task<string> GetPhoneNumbers();
    }
}
