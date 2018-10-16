using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gap.Insurance.Web.ViewModels
{
    public class ErrorMessage
    {
        public List<string> messages { get; set; }
        public object developerMessage { get; set; }
    }
}
