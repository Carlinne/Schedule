using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Schedule
{
    public class Property : BaseModel
    {
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? Disabled_at { get; set; }
    }
}
