namespace API.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddScreeningDto
    {
        public DateTime StartTime { get; set; }

        public int MaxSeatsNumber { get; set; }

        [Required]
        public int MovieId { get; set; }
    }
}
