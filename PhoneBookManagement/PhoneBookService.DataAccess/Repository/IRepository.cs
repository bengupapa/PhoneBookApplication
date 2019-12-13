using ContactManagement.Common.ServiceModels.Request;
using PhoneBookService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookService.DataAccess.Repository
{
    public interface IRepository: IDisposable
    {
        Task<int> GetUserTotalCount(string searchValue);
        Task<IEnumerable<UserProfile>> GetDefaultContacts(PageRequest pageRequest);
        Task<IEnumerable<PhoneNumber>> GetContactsByUserId(int userId);
        Task<int> SaveChangeAsync();
    }
}
