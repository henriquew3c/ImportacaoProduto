using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _1.Cliente.Support
{
    public class MvcValidator : BasicValidation, IValidationState
    {
        public bool FillErrors(ModelStateDictionary modelState)
        {
            foreach (var domainError in DomainErrors)
            {
                modelState.AddModelError(domainError.Key, domainError.Value);
            }

            return modelState.IsValid;
        }

        public bool FillMessages()
        {
            //DomainErrors.ToList().ForEach(error => _showMessages.Show(new Message("Validação", error.Value, MessagesType.Warning)));

            return !DomainErrors.Any();
        }
    }
}