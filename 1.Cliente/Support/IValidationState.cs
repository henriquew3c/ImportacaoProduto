using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _1.Cliente.Support
{
    public interface IValidationState
    {
        bool IsValid { get; }

        bool FillErrors(ModelStateDictionary modelState);

        bool FillMessages();
    }
}