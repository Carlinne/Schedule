using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Schedule
{
    public class Activity : BaseModel
    {
        public int Property_Id { get; set; }
        [ForeignKey("Property_Id")]
        public virtual Property Property { get; set; }

        public DateTime Schedule { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
    }
}
