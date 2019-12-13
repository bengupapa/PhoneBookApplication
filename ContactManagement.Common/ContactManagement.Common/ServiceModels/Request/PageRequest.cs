using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Common.ServiceModels.Request
{
    public class PageRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }
    }
}
