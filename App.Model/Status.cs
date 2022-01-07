using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
