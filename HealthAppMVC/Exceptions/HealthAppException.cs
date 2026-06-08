using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppMVC.Exceptions
{
    public class HealthAppException : Exception
    {
        public HealthAppException(string message) : base(message) { }
    }
}