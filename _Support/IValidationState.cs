using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _Support
{
    public interface IValidationState
    {
        bool IsValid { get; }

        bool FillErrors(ModelStateDictionary modelState);

        bool FillMessages();
    }
}