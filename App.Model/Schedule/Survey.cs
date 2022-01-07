using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Schedule
{
    public class Survey : BaseModel
    {
        public int Activity_Id { get; set; }
        [ForeignKey("Activity_Id")]
        public virtual Activity Activity { get; set; }

        [Required]
        public string Answers { get; set; }
    }
}
