using System;

namespace App.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int StatusId { get; set; }
    }
}
