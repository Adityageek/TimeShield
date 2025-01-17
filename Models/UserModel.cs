﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeShield.Models
{
    public class UserModel 
    {
        
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string District { get; set; }

        public int Pincode { get; set; }

        public string Contact { get; set; }

        public string ProfileType { get; set; }
    }
}