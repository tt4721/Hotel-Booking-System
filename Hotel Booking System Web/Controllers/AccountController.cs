using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Aplikacija.Models;

namespace Hotel_Booking_System_Web.Controllers
{
    public class AccountController : Controller
    {
        
        SqlConnection con = new SqlConnection();
        SqlCommand comLogin = new SqlCommand();
        SqlCommand comRegister = new SqlCommand();
        SqlCommand comAdmin = new SqlCommand(); 
        SqlDataReader loginReader;
        SqlDataReader registerReader;
        SqlDataReader adminReader;

        // GET: Account

        [HttpGet]

        public ActionResult Home()
        {

            return View();
        }

        void connestionString()
        {
            con.ConnectionString = "data source=TADIC; database=Hotel_Booking_System; integrated security=SSPI;";
        }



        [HttpPost]

        public ActionResult Login(Account acc)
        {
            connestionString();
            con.Open();

            


            comLogin.Connection = con;
            comLogin.CommandText = "SELECT * FROM Accounts WHERE User_Email= '" + acc.User_Email + "' AND User_Password= '" + acc.User_Password + "'";
            loginReader = comLogin.ExecuteReader();

            

            if (loginReader.Read())
            {
                int admin = (int)loginReader["Is_Admin"];

                if(admin==1)
                {
                    con.Close();
                    Session["UserEmailAdmin"] = acc.User_Email;
                    return RedirectToAction("Admin", "Reservation");
                }
                con.Close();
                Session["UserEmail"] = acc.User_Email;
                return RedirectToAction("Welcome", "Reservation");

            }

            else
            {
                con.Close();
                TempData["LoginErrorMessage"] = "Invalid login details.";
                return RedirectToAction("Home");
            }



        }


        public ActionResult Register(Account acc)
        {
            connestionString();
            con.Open();
            comRegister.Connection = con;

            comRegister.CommandText = "SELECT * FROM Accounts WHERE User_Email= '" + acc.User_Email + "'";
            registerReader = comRegister.ExecuteReader();


            if (registerReader.Read())
            {
                TempData["RegisterErrorMessage"] = "User already exists.";
                return RedirectToAction("Home");
            }

            else
            {
                con.Close();

                con.Open();
                if (acc.User_Password == acc.User_ConfirmPassword)
                {
                    comRegister.CommandText = "INSERT INTO Accounts VALUES ('" + acc.User_Email + "', '" + acc.User_Password + "', @Is_Admin)";

                    comRegister.Parameters.AddWithValue("@Is_Admin",0);

                    if (comRegister.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        Session["UserEmail"] = acc.User_Email;
                        return RedirectToAction("Welcome", "Reservation");
                    }

                    else
                    {
                        TempData["RegisterErrorMessage"] = "Invalid operation.";
                        return RedirectToAction("Home");
                    }


                }


                else
                {
                    TempData["RegisterErrorMessage"] = "Incorect confirm password.";
                    return RedirectToAction("Home");
                }
            }


        }

        public ActionResult Logout()
        {
            return RedirectToAction("Home", "Account");
        }

        public ActionResult About() 
        { 
            return View("About");
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }
    }
}