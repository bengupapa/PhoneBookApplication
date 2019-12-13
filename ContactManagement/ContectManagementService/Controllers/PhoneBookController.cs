using ContactManagement.Common.ServiceContracts;
using ContactManagement.Common.ServiceModels.Request;
using ContactManagement.Common.ServiceModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private IPhoneBookService PhoneBookService { get; }

        public PhoneBookController(IPhoneBookService phoneBookService)
        {
            PhoneBookService = phoneBookService;
        }

        [HttpPost]
        public async Task<PhoneBookContactsResponseDto> GetPhoneBook([FromBody]PageRequest pageRequest)
        {
            var foo = await PhoneBookService.GetPhoneBook(pageRequest);

            return foo;
        }
    }
}
