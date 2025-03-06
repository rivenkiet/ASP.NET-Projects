﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
		[DisplayName("Street Address")]
		public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
		[DisplayName("Postal Code")]
		public string? PostalCode { get; set; }
		[RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must be numeric")]
		[DisplayName("Phone Number")]
		public string? PhoneNumber { get; set; }


    }
}
