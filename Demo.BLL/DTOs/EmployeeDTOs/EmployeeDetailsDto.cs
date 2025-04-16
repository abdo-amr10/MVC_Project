using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Enums;

namespace Demo.BLL.DTOs.EmployeeDTOs
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }


        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }
        public string? Image { get; set; }

    }
}
