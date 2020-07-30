using System;
namespace JQueryDT.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public DateTime DateOfAdmission { get; set; } = DateTime.Now.AddYears(-20);
    }
}