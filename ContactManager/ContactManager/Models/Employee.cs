using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Employee name")]
        [Required(ErrorMessage = "Employee name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public bool IsMarried { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Please provide a valid phone number")]
        [Phone(ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Employee phone number")]
        public string Phone { get; set; }


        [Display(Name = "Employee salary")]
        [Required(ErrorMessage = "Employee salary is required.")]
        [Range(300, 5000, ErrorMessage = "Employee salary need to be betweeen 300$ and 5000$.")]
        public decimal Salary { get; set; }

    }
}
