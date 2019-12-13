using ContactManagement.Common.ServiceContracts;
using ContactManagement.Common.ServiceModels.Request;
using ContactManagement.Common.ServiceModels.Response;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using PhoneBookService.DataAccess.Repository;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookService
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    public sealed class PhoneBookService : StatelessService, IPhoneBookService
    {
        private IRepository DatabaseRepository { get; }

        public PhoneBookService(StatelessServiceContext context, IRepository databaseRepository)
            : base(context)
        {
            DatabaseRepository = databaseRepository;
        }

        public async Task<PhoneBookContactsResponseDto> GetPhoneBook(PageRequest pageRequest)
        {
            try
            {
                var userCount = await DatabaseRepository.GetUserTotalCount(pageRequest.SearchValue);
                var contacts = await DatabaseRepository.GetDefaultContacts(pageRequest);

                var phoneBook = contacts.Select(c => new PhoneBookContactDto
                {
                    UserProfileId = c.UserProfileId,
                    FirstName = c.FirstName,
                    Surname = c.Surname,
                    PhoneNumbers = c.PhoneNumbers.Select(pn => new PhoneNumberDto
                    {
                        PhoneNumberId = pn.PhoneNumberId,
                        UserProfileId = pn.UserProfileId,
                        DialingCode = pn.DialingCode,
                        SubscriberNumber = pn.SubscriberNumber,
                        IsPrimary = pn.IsPrimary,
                        Description = pn.Description
                    }).ToList()
                }).ToList();

                return new PhoneBookContactsResponseDto
                {
                    PhoneBook = phoneBook,
                    PhoneBookContactsTotalCount = userCount
                };
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

        public async Task<List<PhoneNumberDto>> GetContactsByUserId(int userId)
        {
            var contacts = await DatabaseRepository.GetContactsByUserId(userId);

            var phoneNumbers = contacts.Select(pn => new PhoneNumberDto
            {
                PhoneNumberId = pn.PhoneNumberId,
                UserProfileId = pn.UserProfileId,
                DialingCode = pn.DialingCode,
                SubscriberNumber = pn.SubscriberNumber
            }).ToList();

            return phoneNumbers;
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
           return new[]
           {
                new ServiceInstanceListener((c) =>
                {
                    return new FabricTransportServiceRemotingListener(c, this);
                })
            };
        }
    }
}
