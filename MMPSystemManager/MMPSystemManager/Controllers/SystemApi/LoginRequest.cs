using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Controllers.SystemApi
{
    public class LoginRequest
    {
        public string UserID { get; set; }
        public string Passwd { get; set; }
    }
}
