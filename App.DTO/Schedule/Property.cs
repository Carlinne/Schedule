using System;

namespace App.DTO.Schedule
{
    public class Property : BaseDTO
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public DateTime? Disabled_at { get; set; }
    }
}
