using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Helpers;

namespace API.Entity
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        [Range(0,30)]
        public int MaxSeatsNumber { get; set; } = 30;
        public int MovieId { get; set; }
        public IEnumerable<Ticket> SoldTickets { get; set; }
    }
}