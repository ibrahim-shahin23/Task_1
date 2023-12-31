﻿using System.ComponentModel.DataAnnotations;

namespace Task.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
