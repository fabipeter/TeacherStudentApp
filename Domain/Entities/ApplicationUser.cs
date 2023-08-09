using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class ApplicationUser : IdentityUser<long>
    {
        public string NationalIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }    
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public string TeacherNumber { get; set; }
        public string StudentNumber { get; set; }
        public string Title { get; set; }
        public string Salary { get; set; }
        public bool IsTeacher { get; set; }
    }
}

