using System;
using System.Collections.Generic;
using System.Text;
using ValidationComponent.CustomAttributes;
using System.Text.Json.Serialization;


namespace AttributesExamples.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(15, 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, "The length of field {0} cannot exceed {1} or be less than {2} characters", 2)]
        public string LastName { get; set; }
        [Required]
        [StringLength(15, "The length of field {0} cannot exceed {1} or be less than {2} characters", 2)]
        [RegularExpression(@"^(?:(?:\+44\s?|0)(?:\d\s?){9,11})$", "The field {0} must be a valid 10-digit phone number.")]
        //[JsonIgnore]
        public string PhoneNo { get; set; }
        [Required]
        [StringLength(15, "The length of field {0} cannot exceed {1} or be less than {2} characters", 2)]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", "The field {0} must be a valid email address.")]
        public string Email { get; set; }
        [Required]
        [StringLength(10, "The length of field {0} cannot exceed {1} or be less than {2} characters", 2)]
        [RegularExpression(@"(?i)^(GIR\s?0AA|[A-Z]{1,2}\d[A-Z\d]?\s?\d[A-Z]{2})$")] //^(GIR\s?0AA|(?:[A-Z]{1,2}\d[A-Z\d]?|[A-Z]{1,2}\d{1,2})\s ?\d[A-Z]{2})$
        //[JsonIgnore]
        public string PostCode { get; set; }
    }
}
