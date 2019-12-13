using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookService.DataAccess.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
