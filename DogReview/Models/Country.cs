﻿using System.ComponentModel.DataAnnotations;

namespace DogReview.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Owner> Owners { get; set; }
    }
}
