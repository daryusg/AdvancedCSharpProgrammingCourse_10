using System;
using System.Collections.Generic;
using System.Text;
using ValidationComponent.CustomAttributes;

namespace AttributesExamples.Models
{
    public class Department
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(5,2)]
        public string ShortName { get; set; }
        public string LongName { get; set; }

    }
}
