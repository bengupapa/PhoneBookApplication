using ContactManagement.Common.ServiceModels.Request;
using ContactManagement.Common.ServiceModels.Response;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Common.ServiceContracts
{
    public interface IPhoneBookService : IService
    {
        Task<PhoneBookContactsResponseDto> GetPhoneBook(PageRequest pageRequest);
        Task<List<PhoneNumberDto>> GetContactsByUserId(int userId);
    }
}
