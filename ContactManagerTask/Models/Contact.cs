using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactManagerTask.Models
{
    public class Contact
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public bool Married { set; get; }
        public string Phone { set; get; }
        public decimal Salary { set; get; }
    }
}