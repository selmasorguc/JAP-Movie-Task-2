using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class ScreeningDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int MaxSeatsNumber { get; set; }
        public int MovieId { get; set; }
        public IEnumerable<TicketDto> SoldTickets { get; set; }
    }
}