using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;

namespace API.DTOs
{
    public class AddScreeningDto
    {
        public DateTime StartTime { get; set; }
        public int MaxSeatsNumber { get; set; }

        [Required]
        public int MovieId { get; set; }

    }
}