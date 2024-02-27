using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage="max length 50")]
        [MinLength(4, ErrorMessage = "min length 50")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }

        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
        public int? DepartmentId { get; set; }//foregin key

        public Department Department { get; set; }
    }
}
