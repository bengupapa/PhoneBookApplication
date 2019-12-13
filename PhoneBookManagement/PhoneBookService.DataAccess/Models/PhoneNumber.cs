using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookService.DataAccess.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public int UserProfileId { get; set; }
        public string DialingCode { get; set; }
        public long SubscriberNumber { get; set; }
        public bool IsPrimary { get; set; }
        public string Description { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
