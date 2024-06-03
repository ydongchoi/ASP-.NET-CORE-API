using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CompanyForCreationDto {
        [Required(ErrorMessage = "Company Name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Company Adress is a required field.")]
        [MaxLength(55, ErrorMessage = "Maximum length for the Address is 55 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Country is a required field.")]
        public string? Country { get; init; }

        IEnumerable<EmployeeForCreationDto> Employees;
    }
}
