using Hotel_Booking_System_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Aplikacija.Models;

namespace Hotel_Booking_System_Web.Controllers
{
    public class ReservationController : Controller
    {

        SqlConnection con = new SqlConnection();

        SqlCommand comReserve = new SqlCommand();
        SqlDataReader reserveReader;

        SqlCommand comGetRooms = new SqlCommand();
        SqlDataReader getRoomReader;

        SqlCommand comGetReservations = new SqlCommand();
        SqlDataReader getReservationsReader;

        SqlCommand comDeleteReservation = new SqlCommand();
        SqlDataReader deleteReservationReader;

        SqlCommand comUpdateReservation = new SqlCommand();
        SqlDataReader updateReservationReader;

        SqlCommand comGetAccounts = new SqlCommand();
        SqlDataReader getAccountsReader;


        SqlCommand comAdmin = new SqlCommand();


        SqlCommand comAvailable = new SqlCommand();
        SqlDataReader availableReader;


        // GET: Reservation


        public ActionResult Welcome()
        {
            List<Rooms> rooms = GetRooms();
            List<Reservations> reservations = GetReservations();
      
            ViewBag.UserEmail = Session["UserEmail"];

            WelcomeModel welcomeModel = new WelcomeModel(rooms, reservations);

            return View(welcomeModel);


        }
            void connectionString()
        {
            con.ConnectionString = "data source=TADIC; database=Hotel_Booking_System; integrated security=SSPI;";
        }


        private bool IsOverlap(DateTime newCheckIn, DateTime newCheckOut, DateTime existingCheckIn, DateTime existingCheckOut)
        {
            return (newCheckIn <= existingCheckOut) && (newCheckOut >= existingCheckIn); 
        }



        [HttpPost]

        public ActionResult Reserve(Reservations reservation)
        {

            connectionString();
            con.Open();


            Random random = new Random();
            int randomReservationId = random.Next(100000, 1000000);

            comReserve.Connection = con;
            comReserve.CommandText = "SELECT * FROM Reservations WHERE Room= '" + reservation.Room + "'";
            reserveReader = comReserve.ExecuteReader();

            if (reserveReader.Read())
            {
                DateTime dbCheckIn = reserveReader.GetDateTime(reserveReader.GetOrdinal("Check_In"));
                DateTime dbCheckOut = reserveReader.GetDateTime(reserveReader.GetOrdinal("Check_Out"));

                if (IsOverlap(reservation.Check_In, reservation.Check_Out, dbCheckIn, dbCheckOut))
                {
                    con.Close();
                    TempData["ReservationErrorMessage"] = "This room is already reserved.";
                    return RedirectToAction("Welcome");
                }


            }


            con.Close();



            con.Open();

            //comReserve.Connection = con;
            comReserve.CommandText = "INSERT INTO Reservations (Reservation_Id, First_Name, Last_Name, Check_In, Check_Out, Phone, Room, Email, Total_Price) VALUES (@Reservation_Id, @First_Name, @Last_Name, @Check_In, @Check_Out, @Phone, @Room, @Email, @Total_Price)";
            comReserve.Parameters.AddWithValue("@Reservation_Id", randomReservationId);
            comReserve.Parameters.AddWithValue("@First_Name", reservation.First_Name);
            comReserve.Parameters.AddWithValue("@Last_Name", reservation.Last_Name);
            comReserve.Parameters.AddWithValue("@Check_In", reservation.Check_In);
            comReserve.Parameters.AddWithValue("@Check_Out", reservation.Check_Out);
            comReserve.Parameters.AddWithValue("@Room", reservation.Room);
            comReserve.Parameters.AddWithValue("@Phone", reservation.Phone);
            comReserve.Parameters.AddWithValue("@Email", Session["UserEmail"]);
            comReserve.Parameters.AddWithValue("@Total_Price", 0);

            


                if (comReserve.ExecuteNonQuery() > 0)
                {
                    
                    
                    TempData["ReservationSucessMessage"] = "Room has been successfully reserved. You can see your reservation in Settings.";
                    return RedirectToAction("Welcome");

                }
                else
                {
                    
                    TempData["ReservationErrorMessage"] = "Error in reservation.";
                    return RedirectToAction("Welcome");
                }
            

            

        }



        
        public List<Rooms> GetRooms()
        {
            List<Rooms> rooms = new List<Rooms>();
            connectionString();
            con.Open();
            comGetRooms.Connection = con;
            comGetRooms.CommandText = "SELECT * FROM Rooms";
            getRoomReader = comGetRooms.ExecuteReader();
            while (getRoomReader.Read())
            {
                rooms.Add(new Rooms()
                {
                    Room_Id = (int)getRoomReader["Room_Id"],
                    Bed_Count = (int)getRoomReader["Bed_Count"],
                    WiFi = getRoomReader["WiFi"].ToString(),
                    Balcony = getRoomReader["Balcony"].ToString(),
                    Terrace = getRoomReader["Terrace"].ToString(),
                    Pet_Friendly = getRoomReader["Pet_Friendly"].ToString(),
                    Room_Service = getRoomReader["Room_Service"].ToString(),
                    Air_Condition = getRoomReader["Air_Condition"].ToString(),
                    Floor = (int)getRoomReader["Floor"],
                    Price_Per_Day = (decimal)getRoomReader["Price_Per_Day"]
                });
            }
            con.Close();

            return rooms;

        }


        public List<Reservations> GetReservations()
        {
            List<Reservations> reservations = new List<Reservations>();
            connectionString();
            con.Open();
            comGetReservations.Connection = con;
            comGetReservations.CommandText = "SELECT * FROM Reservations WHERE Email = '" + Session["UserEmail"] + "'";
            getReservationsReader = comGetReservations.ExecuteReader();
            while (getReservationsReader.Read())
            {
                reservations.Add(new Reservations()
                {
                    Reservation_Id = (int)getReservationsReader["Reservation_Id"],
                    First_Name = getReservationsReader["First_Name"].ToString(),
                    Last_Name = getReservationsReader["Last_Name"].ToString(),
                    Check_In = (DateTime)getReservationsReader["Check_In"],
                    Check_Out = (DateTime)getReservationsReader["Check_Out"],
                    Phone = getReservationsReader["Phone"].ToString(),
                    Room = (int)getReservationsReader["Room"],
                    Email = getReservationsReader["Email"].ToString(),
                    Total_Price = (decimal)getReservationsReader["Total_Price"]
                   
                });
            }
            con.Close();

            return reservations;

        }


        public List<Reservations> GetAllReservations()
        {
            List<Reservations> reservations = new List<Reservations>();
            connectionString();
            con.Open();
            comGetReservations.Connection = con;
            comGetReservations.CommandText = "SELECT * FROM Reservations";
            getReservationsReader = comGetReservations.ExecuteReader();
            while (getReservationsReader.Read())
            {
                reservations.Add(new Reservations()
                {
                    Reservation_Id = (int)getReservationsReader["Reservation_Id"],
                    First_Name = getReservationsReader["First_Name"].ToString(),
                    Last_Name = getReservationsReader["Last_Name"].ToString(),
                    Check_In = (DateTime)getReservationsReader["Check_In"],
                    Check_Out = (DateTime)getReservationsReader["Check_Out"],
                    Phone = getReservationsReader["Phone"].ToString(),
                    Room = (int)getReservationsReader["Room"],
                    Email = getReservationsReader["Email"].ToString(),
                    Total_Price = (decimal)getReservationsReader["Total_Price"]

                });
            }
            con.Close();

            return reservations;

        }


        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            connectionString();
            con.Open();
            comGetAccounts.Connection = con;
            comGetAccounts.CommandText = "SELECT * FROM Accounts WHERE User_Email != '" + Session["UserEmailAdmin"] + "' AND User_Email != 'admin@gmail.com'";
            getAccountsReader = comGetAccounts.ExecuteReader();
            while (getAccountsReader.Read())
            {
                accounts.Add(new Account()
                {
                    User_Email = (string)getAccountsReader["User_Email"],
                    Is_Admin = (int)getAccountsReader["Is_Admin"]

                });
            }
            con.Close();

            return accounts;

        }

        [HttpPost]
        public ActionResult DeleteReservation(Reservations reservations)
        {
        
            connectionString();
            con.Open();
            comDeleteReservation.Connection = con;
            comDeleteReservation.CommandText = "DELETE FROM Reservations WHERE Reservation_Id = '" + reservations.Reservation_Id + "'";

            if(comDeleteReservation.ExecuteNonQuery() > 0)
            {
                con.Close();
                TempData["DeleteMessage"] = "Delete Successfully.";
                return RedirectToAction("Welcome");
            }

            con.Close();
            return RedirectToAction("Welcome");

           
        }


        


        [HttpPost]

        public ActionResult UpdateReservation( Reservations reservation)
        {
            connectionString();
            con.Open();

            comReserve.Connection = con;
            comReserve.CommandText = "SELECT * FROM Reservations WHERE Room= '" + reservation.Room + "' AND Reservation_Id !='" + reservation.Reservation_Id + "'";
            
            reserveReader = comReserve.ExecuteReader();

            if (reserveReader.Read())
            {
                DateTime dbCheckIn = reserveReader.GetDateTime(reserveReader.GetOrdinal("Check_In"));
                DateTime dbCheckOut = reserveReader.GetDateTime(reserveReader.GetOrdinal("Check_Out"));

                if (IsOverlap(reservation.Check_In, reservation.Check_Out, dbCheckIn, dbCheckOut))
                {
                    
                    TempData["ReservationUpdateErrorMessage"] = "This room is already reserved.";
                    return RedirectToAction("Welcome");
                }


            }


            con.Close();

            con.Open();
            comUpdateReservation.Connection = con;
            comUpdateReservation.CommandText = "UPDATE Reservations SET First_Name = @First_Name, Last_Name = @Last_Name, Phone = @Phone, Check_In = @Check_In, Check_Out = @Check_Out, Room = @Room WHERE Reservation_Id = @Reservation_Id";
            comUpdateReservation.Parameters.AddWithValue("@Reservation_Id", reservation.Reservation_Id);
            comUpdateReservation.Parameters.AddWithValue("@First_Name", reservation.First_Name);
            comUpdateReservation.Parameters.AddWithValue("@Last_Name", reservation.Last_Name);
            comUpdateReservation.Parameters.AddWithValue("@Check_In", reservation.Check_In);
            comUpdateReservation.Parameters.AddWithValue("@Check_Out", reservation.Check_Out);
            comUpdateReservation.Parameters.AddWithValue("@Room", reservation.Room);
            comUpdateReservation.Parameters.AddWithValue("@Phone", reservation.Phone);


            if(comUpdateReservation.ExecuteNonQuery() > 0)
            {
                TempData["ReservationUpdateSuccessMessage"] = "Update Successfully.";
                return RedirectToAction("Welcome");
            }

            
            
            return RedirectToAction("Welcome");

        }

        [HttpPost]
        public ActionResult SeeAvailability(Rooms room)
        {
            connectionString();
            con.Open();
            comAvailable.Connection = con;
            comAvailable.CommandText = "SELECT * FROM Reservations WHERE Room = '" + room.Room_Id + "'";
            availableReader = comAvailable.ExecuteReader();

            List<Reservations> filteredReservations = new List<Reservations>();

            while (availableReader.Read())
            {
                filteredReservations.Add(new Reservations()
                {
                    Room = (int)availableReader["Room"],
                    Check_In = (DateTime)availableReader["Check_In"],
                    Check_Out = (DateTime)availableReader["Check_Out"]

                });
            }

            con.Close();

            TempData["FilteredReservations"] = filteredReservations;

            return RedirectToAction("Welcome"); ;
        }


        public ActionResult Admin()
        {
            List<Reservations> reservations = GetAllReservations();
            List<Account> accounts = GetAccounts();
            ViewBag.UserEmailAdmin = Session["UserEmailAdmin"];
            WelcomeModel welcomeModel = new WelcomeModel(accounts, reservations);
            return View(welcomeModel);
        }



        [HttpPost]

        public ActionResult AddAdmin(Account account)
        {
            connectionString();
            con.Open();
            comAdmin.Connection = con;
            comAdmin.CommandText = "UPDATE Accounts SET Is_Admin = 1 WHERE User_Email = '" + account.User_Email + "'";




            if (comAdmin.ExecuteNonQuery() > 0)
            {
                con.Close();
                TempData["AddAdminSuccessMessage"] = "Admin Added Successfully.";
                return RedirectToAction("Admin");
            }


            con.Close();
            return RedirectToAction("Admin");
        }

        [HttpPost]  

        public ActionResult RemoveAdmin(Account account)
        {
            connectionString();
            con.Open();
            comAdmin.Connection = con;
            comAdmin.CommandText = "UPDATE Accounts SET Is_Admin = 0 WHERE User_Email = '" + account.User_Email + "'";



            if (comAdmin.ExecuteNonQuery() > 0)
            {
                con.Close();
                TempData["RemoveAdminSuccessMessage"] = "Admin Removed Successfully.";
                return RedirectToAction("Admin");
            }


            con.Close();
            return RedirectToAction("Admin");
        }
    }
    
}