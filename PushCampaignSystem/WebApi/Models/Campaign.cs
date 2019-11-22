﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Campaign
    {
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string PushMessage { get; set; }

        public IEnumerable<Place> Targeting { get; set; }
    }
}
