using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        //public DateTime CreationDate { get; set; }= DateTime.Now;
        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string ImageName { get; set; }
        public int? DepartmentId { get; set; }//foregin key

        public Department Department { get; set; }
    }
}
