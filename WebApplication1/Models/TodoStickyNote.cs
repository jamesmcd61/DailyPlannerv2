﻿namespace WebApplication1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TodoStickyNote
    {
        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
