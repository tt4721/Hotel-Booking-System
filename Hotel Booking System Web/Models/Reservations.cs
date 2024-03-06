using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel_Booking_System_Web.Models
{
    public class Reservations
    {
        public int Reservation_Id { get; set; } 
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public int Room { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total_Price { get; set; }
    }
}