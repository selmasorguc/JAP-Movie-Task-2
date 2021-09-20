using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entity
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        [Range(0,30)]
        public int MaxSeatsNumber { get; set; } = 30;
        public int MovieId { get; set; }
    }
}