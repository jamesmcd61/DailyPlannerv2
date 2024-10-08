﻿namespace WebApplication1.DataModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterDataModel
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }
    }
}
