using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationService.Domain
{
    public class LogInInfomation : BaseEntity
    {
        public string Name { get; set; }

        public string Pwd { get; set; }

        public bool Active { get; set; }
    }
}
