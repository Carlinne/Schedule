using System;

namespace App.DTO
{
    public class SearchParams
    {
        public bool Filter { get; set; }
        public int? StatusId { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
    }
}
