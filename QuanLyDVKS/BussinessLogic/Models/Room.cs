using System;

namespace BussinessLogic.Models
{
    public class Room
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public bool IsOccupied { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
