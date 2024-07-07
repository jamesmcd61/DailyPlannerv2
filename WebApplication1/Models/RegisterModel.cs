﻿namespace WebApplication1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }
    }
}
