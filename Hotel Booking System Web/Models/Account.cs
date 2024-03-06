using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Aplikacija.Models
{
    public class Account
    {

        public string User_Email { get; set; }

        public string User_Password { get; set; }

        public string User_ConfirmPassword { get; set; }

        public int Is_Admin { get; set; }

    }
}