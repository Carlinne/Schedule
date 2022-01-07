using System;

namespace App.DTO.Schedule
{
    public class Activity : BaseDTO
    {
        public int Property_Id { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; }
    }
}
