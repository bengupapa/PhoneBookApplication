using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Common.ServiceModels.Response
{
    public class PhoneBookContactsResponseDto
    {
        public int PhoneBookContactsTotalCount { get; set; }
        public List<PhoneBookContactDto> PhoneBook { get; set; }
    }

    public class PhoneBookContactDto
    {
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
        public string FullName => $"{FirstName} {Surname}";
    }

    public class PhoneNumberDto
    {
        public int PhoneNumberId { get; set; }
        public int UserProfileId { get; set; }
        public string DialingCode { get; set; }
        public long SubscriberNumber { get; set; }
        public bool IsPrimary { get; set; }
        public string ContactNumber => $"{DialingCode}{SubscriberNumber}";
        public string Description { get; set; }
    }
}
