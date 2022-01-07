using System;

namespace App.DTO.Schedule
{
    public class ActivityDetail
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; }
        public DateTime Created_At { get; set; }
        public string Status { get; set; }
        public string Condicion { get; set; }
        public Property Property { get; set; }
        public Survey Survey { get; set; }
    }
}
