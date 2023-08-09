using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Application.Validation;

namespace Application.Account.DTOs
{
    public class RegisterDto 
    {
        [Required]
        public string NationalIdNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [CustomBirthDateValidation]
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        public string TeacherNumber { get; set; }

        public string StudentNumber { get; set; }

        public string Title { get; set; }

        public string Salary { get; set; }

        [Required]
        public bool IsTeacher { get; set; }

       
    }
}

