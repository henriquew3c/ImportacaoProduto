using System.ComponentModel.DataAnnotations;
using _1.Cliente.Support.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace _1.Cliente.Application
{
    public class CreateImportacoesRequest : IRequest
    {
        [Required(ErrorMessage = "O campo Arquivo é obrigatório.")]
        [AllowedExtensions(new string[] { ".xls", ".xlsx" })]
        [MaxFileSize(268435456)] // 256 Megabytes
        [DataType(DataType.Upload)]
        [Display(Name = "Arquivo*")]
        public IFormFile Arquivo { get; set; }

    }
}
