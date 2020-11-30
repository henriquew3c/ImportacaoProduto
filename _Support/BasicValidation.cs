using System;
using System.Collections.Generic;
using System.Linq;

namespace _Support
{
    public class BasicValidation : IDomainValidation
    {
        protected internal readonly Dictionary<string, string> DomainErrors = new Dictionary<string, string>();

        public bool IsValid => DomainErrors.Any() == false;

        public void AddError(string message)
        {
            DomainErrors.Add(Guid.NewGuid().ToString(), message);
        }
    }
}