using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Aplikacija.Models;

namespace Hotel_Booking_System_Web.Models
{
    public class WelcomeModel
    {
        public List<Rooms> Rooms { get; set; }
        public List<Reservations> Reservations { get; set; }
        public List<Account> Accounts { get; set; }
        

        public WelcomeModel( List<Rooms> rooms, List<Reservations> reservations )
        {
            Rooms = rooms;
            Reservations = reservations;
            
        }

        

        public WelcomeModel ( List <Account> accounts, List<Reservations> reservations )
        {
            Accounts = accounts;
            Reservations = reservations;
        }
    }
}