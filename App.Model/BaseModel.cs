using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
