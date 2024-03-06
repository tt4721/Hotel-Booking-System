using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Hotel_Booking_System_Web.Models
{
    public class Rooms
    {
        public int Room_Id { get; set; }
        public int Bed_Count { get; set; }
        public string WiFi { get; set; }
        public string Balcony { get; set; }
        public string Pet_Friendly { get; set; }
        public string Room_Service {  get; set; }
        public string Terrace { get; set; }
        public string Air_Condition { get; set; }
        public string Smart_TV { get; set; }
        public int Floor { get; set; }
        public decimal Price_Per_Day { get; set; }
    }
}