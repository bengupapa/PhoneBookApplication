using ContactManagement.Common.ServiceModels.Request;
using Microsoft.EntityFrameworkCore;
using PhoneBookService.DataAccess.Context;
using PhoneBookService.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookService.DataAccess.Repository
{
    public sealed class Repository : IRepository
    {
        private ContactsInformationDatabaseContext DatabaseContext { get; }

        public Repository(ContactsInformationDatabaseContext databaseContext) => DatabaseContext = databaseContext;

        public async Task<IEnumerable<PhoneNumber>> GetContactsByUserId(int userId)
        {
            var userContacts = await DatabaseContext.UserProfile
                .Where(u => u.UserProfileId == userId)
                .SelectMany(u => u.PhoneNumbers)
                .ToListAsync();

            return userContacts;
        }

        public async Task<int> GetUserTotalCount(string searchValue)
        {
            var userProfiles = DatabaseContext.UserProfile.AsQueryable();

            if(!string.IsNullOrWhiteSpace(searchValue))
            {
                userProfiles = userProfiles.Where(u =>
                        u.FirstName.Contains(searchValue) ||
                        u.Surname.Contains(searchValue));
            }

            return await userProfiles.CountAsync();
        }

        public async Task<IEnumerable<UserProfile>> GetDefaultContacts(PageRequest pageRequest)
        {
            var contacts = DatabaseContext
                .UserProfile
                .Include(u => u.PhoneNumbers).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pageRequest.SearchValue))
            {
                contacts = contacts.Where(u =>
                        u.FirstName.Contains(pageRequest.SearchValue) ||
                        u.Surname.Contains(pageRequest.SearchValue));
            }

            contacts = contacts
                .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
                .Take(pageRequest.PageSize);

            return await contacts.ToListAsync();
        }

        public void Dispose()
        {
            DatabaseContext.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await DatabaseContext.SaveChangesAsync();
        }
    }
}
