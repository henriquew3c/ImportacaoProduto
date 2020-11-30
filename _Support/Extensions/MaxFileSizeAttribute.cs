using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace _Support.Extensions
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if (file == null) return ValidationResult.Success;
            
            return file.Length > _maxFileSize ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"O tamanho máximo para o arquivo é { _maxFileSize} bytes.";
        }
    }
}