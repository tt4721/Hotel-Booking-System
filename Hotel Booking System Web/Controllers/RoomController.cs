using Hotel_Booking_System_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Booking_System_Web.Controllers
{
    public class RoomController : Controller
    {
        
        SqlConnection con = new SqlConnection();
        SqlCommand comGetRooms = new SqlCommand();
        SqlDataReader getRoomReader;
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        void connestionString()
        {
            con.ConnectionString = "data source=TADIC; database=Hotel_Booking_System; integrated security=SSPI;";
        }


        
    }
}