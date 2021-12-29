using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models.Attributes;

namespace EmployeeManagement.Models.ViewModels
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "FirstName is mandatory")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "testDomain.com")]
        public string Email { get; set; }

        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "testDomain.com")]
        [Compare("Email", ErrorMessage = "Email is not equals!")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int? DepartmentId { get; set; }
        public string PhotoPath { get; set; }

        //[ValidateComplexType]
        public Department? Department { get; set; }
    }
}
